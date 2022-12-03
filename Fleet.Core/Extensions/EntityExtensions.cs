using System;
using System.Linq;
using Fleet.Core.Entities;

namespace Fleet.Core.Extensions
{
    public static class EntityExtensions
    {
        public static string GetMemberName<T>( this T entity, string propertyName ) where T : BaseEntity
        {
            var properties = entity.GetType().GetProperties();
            var property = properties.First ( x => x.Name == propertyName );
            return  property.Name;
        }
    }
}