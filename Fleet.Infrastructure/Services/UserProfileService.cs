using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Fleet.Core.ApiModels;
using Fleet.Core.Dtos;
using Fleet.Core.Entities;
using Fleet.Core.Enums;
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
                "LEFT JOIN przychody_użytkownika prz ON prz.AccountId = pu.AccountId " +
                "SET stan_konta = stan_konta + " +
                "   (SELECT SUM(kwota_przychodu) FROM przychody_użytkownika prz1 " +
                "   WHERE date(kolejny_przychód) = current_date and prz.AccountId = prz1.AccountId), " +
                "   kolejny_przychód = date_add(kolejny_przychód, interval cykliczność_dni DAY) " +
                "WHERE date(kolejny_przychód) = current_date";
            

            var updateOutcomeQuery =
                "UPDATE profil_użytkownika pu " +
                "LEFT JOIN opłaty_użytkownika opl ON opl.AccountId = pu.AccountId " +
                "SET stan_konta = stan_konta - " +
                "   (SELECT SUM(opl1.kwota_płatności) FROM opłaty_użytkownika opl1 " +
                "   WHERE date(opl1.kolejna_płatność) = current_date and opl.AccountId = opl1.AccountId), " +
                "   kolejna_płatność = date_add(kolejna_płatność, interval cykliczność_dni DAY) " +
                "WHERE date(kolejna_płatność) = current_date";
            
            await _unitOfWork.Repository<UserProfileEntity>().ExecuteNonQuery ( updateIncomeQuery );
            await _unitOfWork.Repository<UserProfileEntity>().ExecuteNonQuery ( updateOutcomeQuery );
        }

        public async Task<ApiResponse<IncomeDto>> CreateIncomeAsync( IncomeDto incomeDto )
        {
            var validateResult = await Validate ( incomeDto, EOperationEntity.Add );
            if( !string.IsNullOrEmpty ( validateResult ) )
                return new ApiResponse<IncomeDto> ( 400, validateResult, null );
            
            var income = _map.Map<IncomeEntity> ( incomeDto );
            _unitOfWork.Repository<IncomeEntity>().Add ( income );

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? new ApiResponse<IncomeDto>(200, "", incomeDto) : null;
        }

        public async Task<ApiResponse> DeleteIncome( IncomeLittleDto incomeDto )
        {
            var income = await GetIncomeAsync ( incomeDto.Source, incomeDto.AccountId );
            if( income != null )
            {
                _unitOfWork.Repository<IncomeEntity>().Delete ( income );
                await _unitOfWork.CompleteAsync();
            }

            return new ApiResponse ( 200, "" );
        }

        // TODO: Dodać osobny update dla nazwy płatności
        // TODO: Dodać osobny update dla terminu kolejnej płatności
        public async Task<ApiResponse<IncomeDto>> UpdateIncomeAsync( IncomeDto incomeDto )
        {
            var validateResult = await Validate ( incomeDto, EOperationEntity.Update );
            if( !string.IsNullOrEmpty ( validateResult ) )
                return new ApiResponse<IncomeDto> ( 400, validateResult, null );

            var entity = await GetIncomeAsync ( incomeDto.Source, incomeDto.AccountId );
            entity.Income = incomeDto.Income;
            entity.Source = incomeDto.Source;
            entity.NextIncome = entity.PeriodicityDay < incomeDto.PeriodicityDay ? 
                entity.NextIncome.AddDays ( -(entity.PeriodicityDay - incomeDto.PeriodicityDay) ) : 
                entity.NextIncome.AddDays ( incomeDto.PeriodicityDay - entity.PeriodicityDay );
            entity.PeriodicityDay = incomeDto.PeriodicityDay;
            
            if( entity.NextIncome.Date <= DateTime.Today )
                return new ApiResponse<IncomeDto> ( 200, "Aktualizacja cykliczności przychodu na podaną ilość dni zaktualizuje dzień przychodu na wcześniejszy od bieżącego. Poczekaj na wpływ środków.", null );
            
            _unitOfWork.Repository<IncomeEntity>().Update ( entity );
            await _unitOfWork.CompleteAsync();

            var updatedEntity = _map.Map<IncomeDto> ( entity );


            return new ApiResponse<IncomeDto> ( 200, "", updatedEntity );
        }

        public async Task<IncomeEntity> GetIncomeAsync( string source, int accountId )
        {
            var spec = new UserProfileSpecification ( source, accountId );
            var income = await _unitOfWork.Repository<IncomeEntity>().GetEntityWithSpecAsync ( spec );
            return income;
        }

        private async Task<bool> IsIncomeExistAsync( string source, int accountId )
        {
            var spec = new UserProfileSpecification ( source, accountId );
            var entity = await _unitOfWork.Repository<IncomeEntity>().GetEntityWithSpecAsync ( spec );

            return entity != null;
        }

        public async Task<IReadOnlyList<IncomeDto>> GetIncomesAsync( int accountId )
        {
            var spec = new UserProfileSpecification ( accountId );
            var incomes = await _unitOfWork.Repository<IncomeEntity>().ListAsync ( spec );
            var incomesDto = _map.Map<IReadOnlyList<IncomeDto>> ( incomes );
            return incomesDto;
        }

        /// <summary>
        /// Waliduje wymagalność pól dla danej operacji
        /// </summary>
        /// <param name="incomeDto">Obiekt do sprawdzenia</param>
        /// <param name="operation">Typ operacji</param>
        /// <returns>Pusty string jeśli wszystko OK lub informacja co poszło nie tak</returns>
        private async Task<string> Validate( IncomeDto incomeDto, EOperationEntity operation )
        {
            switch ( operation )
            {
                case EOperationEntity.Add:
                    if( incomeDto.Income <= 0 )
                        return "Przychód nie może być ujemny";
                    if( await GetIncomeAsync ( incomeDto.Source, incomeDto.AccountId ) != null )
                        return $"Przychód o nazwie '{incomeDto.Source}' już istnieje.";
                    if( incomeDto.PeriodicityDay <= 0 || incomeDto.PeriodicityDay > new GregorianCalendar().GetDaysInYear(DateTime.Now.Year) )
                        return "Cykliczność musi się mieścić w przedziale między 1 dzień a ilością dni w roku";
                    if( incomeDto.NextIncome.Date <= DateTime.Today )
                        return "Kolejny przychód może być ustawiony od co najmniej kolejnego dnia";
                    break;
                
                case EOperationEntity.Update:
                    if( incomeDto.Income <= 0 )
                        return "Przychód nie może być ujemny";
                    if( incomeDto.PeriodicityDay <= 0 || incomeDto.PeriodicityDay > new GregorianCalendar().GetDaysInYear(DateTime.Now.Year) )
                        return "Cykliczność musi się mieścić w przedziale między 1 dzień a ilością dni w roku";
                    if( !await IsIncomeExistAsync ( incomeDto.Source, incomeDto.AccountId ) )
                        return $"Brak przychodu o nazwie {incomeDto.Source}";
                    if( incomeDto.NextIncome.Date <= DateTime.Today )
                        return "Kolejny przychód może być ustawiony od co najmniej kolejnego dnia";
                    break;
            }

            return string.Empty;
        }
    }
}