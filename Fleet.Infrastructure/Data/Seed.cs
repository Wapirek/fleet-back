using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Fleet.Infrastructure.Data
{
    public class Seed
    {
        #region Private Members

        private readonly FleetContext _context;

        #endregion
        
        #region Constructor

        public Seed(FleetContext context)
        {
            _context = context;
        }
        
        #endregion

        public void SeedData()
        {
            if( _context.Accounts.Any() ) return;
            
            var employeeData = File.ReadAllText( "../Fleet.Infrastructure/Data/DataSeed.json" );
            
            var model = JsonConvert.DeserializeObject<DataModel>( employeeData );
            
            #region Account
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashSalt( "Password", out passwordHash, out passwordSalt );
            model.Account.Hash = passwordHash;
            model.Account.PasswordSalt = passwordSalt;
            model.Account.Created = DateTime.Now;
            
            _context.Accounts.Add( model.Account );
            
            #endregion
            
            #region User Profile

            _context.UserProfile.Add ( model.UserProfile );
            
            #endregion
            
            #region Incomes
            
            foreach ( var income in model.Incomes )
            {
                income.Account = model.Account;
                income.NextIncome = DateTime.Now.AddDays ( income.PeriodicityDay );
                _context.Incomes.Add ( income );
            }
            
            #endregion
            
            #region Outcomes
            
            foreach ( var outcome in model.Outcomes )
            {
                outcome.Account = model.Account;
                outcome.NextOutcome = DateTime.Now.AddDays ( outcome.PeriodicityDay );
                _context.Outcomes.Add ( outcome );
            }
            
            #endregion
            
            #region Catalogs

            foreach ( var catalog in model.Catalogs )
            {
                catalog.Account = model.Account;
                _context.Catalogs.Add ( catalog );
            }
            
            #endregion
            
            #region Products

            foreach ( var product in model.Products )
            {
                product.Account = model.Account;
                _context.Products.Add ( product );
            }
            
            #endregion
            
            #region Transactions

            foreach ( var transaction in model.Transactions )
            {
                transaction.Account = model.Account;
                _context.Transactions.Add ( transaction );
            }
            
            #endregion
            
            _context.SaveChanges();
        }
        
        private void CreatePasswordHashSalt ( string password, out byte[] passwordHash, out byte[] passwordSalt )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash( Encoding.UTF8.GetBytes( password ) );
            }
        }
    }
}