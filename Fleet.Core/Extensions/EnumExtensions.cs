using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Fleet.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string? GetnEnumMemberValue<T>( this T value ) where T : Enum
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault ( x => x.Name == value.ToString() )
                ?.GetCustomAttribute<EnumMemberAttribute> ( false )
                ?.Value;
        }
    }
}