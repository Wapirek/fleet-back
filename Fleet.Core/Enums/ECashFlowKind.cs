using System.Runtime.Serialization;

namespace Fleet.Core.Enums
{
    public enum ECashFlowKind
    {
        [EnumMember(Value = "Przychód")]
        Income,
        [EnumMember(Value = "Płatność")]
        Przychód
    }
}