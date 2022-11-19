using System.Runtime.Serialization;

namespace Fleet.Core.Enums
{
    public enum ETransactionDirection
    {
        [EnumMember(Value = "Wydatek")]
        Cost,
        [EnumMember(Value = "Zarobek")]
        Earn
    }
}