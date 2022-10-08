﻿using System.Linq;
using System.Threading.Tasks;
using Fleet.Core.Entities;
using Fleet.Core.Interfaces.Repositories;
using Fleet.Core.Specifications;
using Fleet.Infrastructure.Data;

namespace Fleet.Infrastructure.Repositories
{
    /// <summary>
    /// Wspólne metody do wykonywania poleceń SQL dla wszystkich tabel
    /// </summary>
    /// <typeparam name="T">Tabela encji</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        #region Private Members

        private readonly FleetContext _context;

        #endregion
        
        #region Constructors

        public GenericRepository(FleetContext context)
        {
            _context = context;
        }
        
        #endregion
        
        #region Implemented Methods
        
        public async Task<T> GetByIdAsync( int id )
        {
            return (await _context.Set<T>().FindAsync ( id ))!;
        }

        public void Add( T entity )
        {
            _context.Set<T>().Add ( entity );
        }

        public void Update( T entity )
        {
            _context.Set<T>().Update ( entity );
        }

        public void Delete( T entity )
        {
            _context.Set<T>().Remove ( entity );
        }
        
        #endregion
        
        #region Private Methods

        /// <summary>
        /// Zbiera wszyskie specyfikacje do zapytania z tabeli T
        /// </summary>
        /// <param name="spec">Zbiór ustawionych metod SQL</param>
        /// <returns>Kompletne zapytanie SQL</returns>
        private IQueryable<T> ApplySpecification( ISpecification<T> spec )
        {
            return SpecificationEvaluator<T>.GetQuery ( _context.Set<T>().AsQueryable(), spec );
        }
        
        #endregion
    }
}