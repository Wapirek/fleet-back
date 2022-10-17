using System;
using System.Collections;
using System.Threading.Tasks;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Infrastructure.Data;

namespace Fleet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members
        
        private readonly FleetContext _context;
        private Hashtable _repositories;

        #endregion
        
        #region Constructors
        
        public UnitOfWork(FleetContext context)
        {
            _context = context;
        }
        
        #endregion
        
        #region Implemented Methods
        
        /// <summary>
        /// Odzyskiwanie póli połączeń
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            // Inicjalizacja kolekcji klucz-wartość do przechowania encji
            _repositories ??= new Hashtable();

            var type = typeof(TEntity).Name;

            // Jeśli typ dziedziczy po BaseEntity..
            if( !_repositories.ContainsKey ( type ) )
            {
                // Stwórz instacje repozytorium jako wartość dla kontekstu
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance ( 
                    repositoryType.MakeGenericType ( typeof(TEntity) ),
                    _context 
                );

                // Dodaje wiersz tabeli do zapisu
                _repositories.Add ( type, repositoryInstance );
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            // kolekcja encji jest zapisywana
            return await _context.SaveChangesAsync();
        }
        
        #endregion
    }
}