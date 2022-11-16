using System;

namespace Fleet.Core.Dtos
{
    public class CashFlowDto : CashFlowLittleDto
    {
        public double Charge { get; set; }
        public int PeriodicityDay { get; set; }
        public DateTime NextCashFlow { get; set; }
        public string CashFlowKind { get; set; }
    }
}