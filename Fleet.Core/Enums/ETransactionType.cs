using System.Runtime.Serialization;

namespace Fleet.Core.Enums
{
    public enum ETransactionType
    {
        [EnumMember(Value="Prosta")]
        Simply,
        [EnumMember(Value = "Złożona")]
        Complex
    }
}