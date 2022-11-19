using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Entities;
using Fleet.Core.Enums;
using Fleet.Core.Extensions;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;
using Fleet.Core.Specifications;

namespace Fleet.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public UserProfileService( IUnitOfWork unitOfWork,
            IMapper map)
        {
            _unitOfWork = unitOfWork;
            _map = map;
        }

        public async Task UpdateAccountBalanceAsync()
        {
            var updateIncomeQuery =
                "UPDATE profil_użytkownika pu " +
                "LEFT JOIN przepływy_pieniężne pp ON pp.AccountId = pu.AccountId " +
                "SET stan_konta = stan_konta + " +
                "   (SELECT SUM(obciążenie) FROM przepływy_pieniężne pp1 " +
                "   WHERE date(kolejny_przepływ) = current_date and pp.AccountId = pp1.AccountId) AND rodzaj_przepływu = 'Przychód', " +
                "   kolejny_przepływ = date_add(kolejny_przepływ, interval cykliczność_dni DAY) " +
                "WHERE date(kolejny_przepływ) = current_date";
            

            var updateOutcomeQuery =
                "UPDATE profil_użytkownika pu " +
                "LEFT JOIN przepływy_pieniężne pp ON pp.AccountId = pu.AccountId " +
                "SET stan_konta = stan_konta - " +
                "   (SELECT SUM(pp1.obciążenie) FROM przepływy_pieniężne pp1 " +
                "   WHERE date(pp1.kolejny_przepływ) = current_date and pp1.AccountId = pp1.AccountId)  AND rodzaj_przepływu = 'Płatność', " +
                "   kolejny_przepływ = date_add(kolejny_przepływ, interval cykliczność_dni DAY) " +
                "WHERE date(kolejny_przepływ) = current_date";
            
            await _unitOfWork.Repository<UserProfileEntity>().ExecuteNonQuery ( updateIncomeQuery );
            await _unitOfWork.Repository<UserProfileEntity>().ExecuteNonQuery ( updateOutcomeQuery );
        }

        public async Task UpdateAccountBalanceAsync( int accountId, double paid, int transactionDirectionId )
        {
            var userProfile = await _unitOfWork.Repository<UserProfileEntity>().GetByIdAsync ( accountId );
            var transactionDirection = await _unitOfWork.Repository<TransactionDirectionEntity>().GetByIdAsync ( transactionDirectionId );
            if( userProfile != null )
            {
                if( transactionDirection.TransactionDirection == ETransactionDirection.Cost.GetnEnumMemberValue() )
                    userProfile.AccountBalance -= paid;
                if( transactionDirection.TransactionDirection == ETransactionDirection.Earn.GetnEnumMemberValue() )
                    userProfile.AccountBalance += paid;

                _unitOfWork.Repository<UserProfileEntity>().Update ( userProfile );
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<ApiResponse<CashFlowDto>> CreateCashFlowAsync( CashFlowDto cashFlowDto )
        {
            var validateResult = await Validate ( cashFlowDto, EOperationEntity.Add );
            if( !string.IsNullOrEmpty ( validateResult ) )
                return new ApiResponse<CashFlowDto> ( 400, validateResult, null );
            
            var cashFlow = _map.Map<CashFlowEntity> ( cashFlowDto );
            _unitOfWork.Repository<CashFlowEntity>().Add ( cashFlow );

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? new ApiResponse<CashFlowDto>(200, "", cashFlowDto) : null;
        }

        public async Task<ApiResponse> DeleteCashFlowAsync( CashFlowLittleDto cashFlowDto )
        {
            var income = await GetCashFlowAsync ( cashFlowDto.Source, cashFlowDto.AccountId );
            if( income != null )
            {
                _unitOfWork.Repository<CashFlowEntity>().Delete ( income );
                await _unitOfWork.CompleteAsync();
            }

            return new ApiResponse ( 200, "" );
        }

        // TODO: Dodać osobny update dla nazwy płatności
        // TODO: Dodać osobny update dla terminu kolejnej płatności
        public async Task<ApiResponse<CashFlowDto>> UpdateCashFlowAsync( CashFlowDto cashFlowDto )
        {
            var validateResult = await Validate ( cashFlowDto, EOperationEntity.Update );
            if( !string.IsNullOrEmpty ( validateResult ) )
                return new ApiResponse<CashFlowDto> ( 400, validateResult, null );

            var entity = await GetCashFlowAsync ( cashFlowDto.Source, cashFlowDto.AccountId );
            entity.Charge = cashFlowDto.Charge;
            entity.Source = cashFlowDto.Source;
            entity.NextCashFlow = entity.PeriodicityDay < cashFlowDto.PeriodicityDay ? 
                entity.NextCashFlow.AddDays ( -(entity.PeriodicityDay - cashFlowDto.PeriodicityDay) ) : 
                entity.NextCashFlow.AddDays ( cashFlowDto.PeriodicityDay - entity.PeriodicityDay );
            entity.PeriodicityDay = cashFlowDto.PeriodicityDay;
            
            if( entity.NextCashFlow.Date <= DateTime.Today )
                return new ApiResponse<CashFlowDto> ( 200, "Aktualizacja cykliczności przychodu na podaną ilość dni zaktualizuje dzień przychodu na wcześniejszy od bieżącego. Poczekaj na wpływ środków.", null );
            
            _unitOfWork.Repository<CashFlowEntity>().Update ( entity );
            await _unitOfWork.CompleteAsync();

            var updatedEntity = _map.Map<CashFlowDto> ( entity );


            return new ApiResponse<CashFlowDto> ( 200, "", updatedEntity );
        }

        public async Task<CashFlowEntity> GetCashFlowAsync( string source, int accountId )
        {
            var spec = new CashFlowSpecification ( source, accountId );
            var cashFlow = await _unitOfWork.Repository<CashFlowEntity>().GetEntityWithSpecAsync ( spec );
            return cashFlow;
        }

        private async Task<bool> IsCashFlowExistAsync( string source, int accountId )
        {
            var spec = new CashFlowSpecification ( source, accountId );
            var entity = await _unitOfWork.Repository<CashFlowEntity>().GetEntityWithSpecAsync ( spec );

            return entity != null;
        }

        public async Task<IReadOnlyList<CashFlowDto>> GetCashFlowsAsync( int accountId )
        {
            var spec = new CashFlowSpecification ( accountId );
            var cashFlow = await _unitOfWork.Repository<CashFlowEntity>().ListAsync ( spec );
            var cashFlowDto = _map.Map<IReadOnlyList<CashFlowDto>> ( cashFlow );
            return cashFlowDto;
        }

        /// <summary>
        /// Waliduje wymagalność pól dla danej operacji
        /// </summary>
        /// <param name="cashFlowDto">Obiekt do sprawdzenia</param>
        /// <param name="operation">Typ operacji</param>
        /// <returns>Pusty string jeśli wszystko OK lub informacja co poszło nie tak</returns>
        private async Task<string> Validate( CashFlowDto cashFlowDto, EOperationEntity operation )
        {
            switch ( operation )
            {
                case EOperationEntity.Add:
                    if( cashFlowDto.Charge <= 0 )
                        return "Przepływ nie może być ujemny";
                    if( await GetCashFlowAsync ( cashFlowDto.Source, cashFlowDto.AccountId ) != null )
                        return $"Przepływ o nazwie '{cashFlowDto.Source}' już istnieje.";
                    if( cashFlowDto.PeriodicityDay <= 0 || cashFlowDto.PeriodicityDay > new GregorianCalendar().GetDaysInYear(DateTime.Now.Year) )
                        return "Cykliczność musi się mieścić w przedziale między 1 dzień a ilością dni w roku";
                    if( cashFlowDto.NextCashFlow.Date <= DateTime.Today )
                        return "Kolejny przepływ może być ustawiony od co najmniej kolejnego dnia";
                    break;
                
                case EOperationEntity.Update:
                    if( cashFlowDto.Charge <= 0 )
                        return "Przepływ nie może być ujemny";
                    if( cashFlowDto.PeriodicityDay <= 0 || cashFlowDto.PeriodicityDay > new GregorianCalendar().GetDaysInYear(DateTime.Now.Year) )
                        return "Cykliczność musi się mieścić w przedziale między 1 dzień a ilością dni w roku";
                    if( !await IsCashFlowExistAsync ( cashFlowDto.Source, cashFlowDto.AccountId ) )
                        return $"Brak Pprzepływu o nazwie {cashFlowDto.Source}";
                    if( cashFlowDto.NextCashFlow.Date <= DateTime.Today )
                        return "Kolejny przepływ może być ustawiony od co najmniej kolejnego dnia";
                    break;
            }

            return string.Empty;
        }
    }
}