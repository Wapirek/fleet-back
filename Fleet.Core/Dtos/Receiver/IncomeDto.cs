using System;

namespace Fleet.Core.Dtos
{
    public class IncomeDto : IncomeLittleDto
    {
        public double Income { get; set; }
        public int PeriodicityDay { get; set; }
        public DateTime NextIncome { get; set; }
    }
}