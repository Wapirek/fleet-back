using System.Threading.Tasks;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Interfaces.Services;

namespace Fleet.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
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
    }
}