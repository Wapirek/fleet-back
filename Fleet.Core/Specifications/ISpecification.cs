using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fleet.Core.Specifications
{
    /// <summary>
    /// Obsługa słów kluczowych SQL do budowania zapytań
    /// </summary>
    /// <typeparam name="T">Tabela</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Słowo kluczowe WHERE
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }
        
        /// <summary>
        /// Słowo kluczowe JOIN
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }
        
        /// <summary>
        /// Słowo kluczowe ORDER BY object ASC
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; }
        
        /// <summary>
        /// Słowo kluczowe ORDER BY object DESC
        /// </summary>
        Expression<Func<T, object>> OrderByDesc { get; }
        
        /// <summary>
        /// Słowo kluczowe LIMIT
        /// </summary>
        int Take { get; }
        
        /// <summary>
        /// Słowo kluczowe
        /// </summary>
        int Skip { get; }
        
        /// <summary>
        /// Sprawdza czy strona zawiera dane
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}