﻿using System.Threading.Tasks;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Repositories
{
    /// <summary>
    /// Wspólne metody do wykonywania poleceń SQL dla wszystkich tabel
    /// </summary>
    /// <typeparam name="T">Tabela encji</typeparam>
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Select * from T where columnId = id
        /// </summary>
        /// <param name="id">Klucz główny tabeli T</param>
        /// <returns>Wiersz tabeli o danym id</returns>
        Task<T> GetByIdAsync( int id );
        
        /// <summary>
        /// Przechowuje lokalnie rekord w tabeli przed zapisem do bazy danych
        /// </summary>
        /// <param name="entity"></param>
        void Add( T entity );

        /// <summary>
        /// Przechowuje lokalnie rekord w tabeli przed aktualizacją tego wiersza w tabeli
        /// </summary>
        /// <param name="entity"></param>
        void Update( T entity );

        /// <summary>
        /// Przechowuje lokalnie rekord do usunięcia przed zatwierdzeniem usunięcia w bazie danych
        /// </summary>
        /// <param name="entity"></param>
        void Delete( T entity );
    }
}