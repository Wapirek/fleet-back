using System.Linq;
using Fleet.Core.Entities;
using Fleet.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infrastructure
{
    /// <summary>
    /// Builder zbierający segmenty zapytań SQL przed wywołaniem zapytania
    /// </summary>
    /// <typeparam name="TEntity">Tabela</typeparam>
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Dla podanego zapytania uwzględnia przekazane metody SQL
        /// </summary>
        /// <param name="inputQuery">Zapytanie SQL</param>
        /// <param name="spec">Składowe zapytania SQL</param>
        /// <returns>Zbudowane zapytanie</returns>
        public static IQueryable<TEntity> GetQuery( IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec )
        {
            var query = inputQuery;

            if( spec.Criteria != null )
                query = query.Where ( spec.Criteria );

            if( spec.OrderBy != null )
                query = query.OrderBy ( spec.OrderBy );

            if( spec.OrderByDesc != null )
                query = query.OrderByDescending ( spec.OrderByDesc );

            if( spec.IsPagingEnabled )
                query = query.Skip ( spec.Skip ).Take ( spec.Take );

            query = spec.Includes.Aggregate ( query,
                ( current, include ) => current.Include ( include ) );

            return query;
        }
    }
}