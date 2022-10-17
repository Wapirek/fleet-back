using System;
using System.Threading.Tasks;
using Fleet.Core.Entities;

namespace Fleet.Core.Interfaces.Repositories
{
    /// <summary>
    /// Główna siedziba zarządzania encjami
    /// Przechowuje kolekcje operowanych encji przed zapisem stanu
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Tworzy instancje wiersza tabeli TEntity umożliwiającą operacje CRUD przed zapisem
        /// </summary>
        /// <typeparam name="TEntity">Tabela</typeparam>
        /// <returns>Zdefiniowane zapytanie na danej tabeli</returns>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        
        /// <summary>
        /// Zapisuje stan wszystkich zebranych encji do bazy danych
        /// </summary>
        /// <returns>Ilość wierszy zapisanych z sukcesem</returns>
        Task<int> CompleteAsync();
    }
}