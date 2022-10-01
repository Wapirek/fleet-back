using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fleet.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        #region Constructors

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        
        #endregion

        #region Implemented Methods
        
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDesc { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        
        #endregion
        
        #region Protected Methods

        /// <summary>
        /// Dodaje JOIN do danego zapytania
        /// </summary>
        /// <param name="includeExpression">Tabela jaka ma zostać dodana</param>
        protected void AddInclude( Expression<Func<T, object>> includeExpression )
        {
            Includes.Add ( includeExpression );
        }

        /// <summary>
        /// Dodaje ORDER BY object ASC do zapytania
        /// </summary>
        /// <param name="orderByExpression">Kolumna po której ma być sortowanie</param>
        protected void AddOrderBy( Expression<Func<T, object>> orderByExpression )
        {
            OrderBy = orderByExpression;
        }
        
        /// <summary>
        /// Dodaje ORDER BY object DESC do zapytania
        /// </summary>
        /// <param name="orderByDescExpression">Kolumna po której ma być sortowanie</param>
        protected void AddOrderByDesc( Expression<Func<T, object>> orderByDescExpression )
        {
            OrderByDesc = orderByDescExpression;
        }
        
        /// <summary>
        /// Zatwierdza, że dana strona zawiera dane
        /// </summary>
        /// <param name="skip">Ilość rekordów do pominięcia</param>
        /// <param name="take">Ilość rekordów na stronie</param>
        protected void ApplyPaging( int skip, int take )
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        
        #endregion
    }

}