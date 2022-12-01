using System;
using System.Collections.Generic;

namespace Fleet.Core.Dtos
{
    public class TransactionDto
    {
        public DateTime TransactionDate { get; set; }
        public string Currency { get; set; }
        public double TotalPaid { get; set; }
        public string TransactionName { get; set; }
        public string Place { get; set; }
        public int AccountId { get; set; }
        public int TransactionDirectionId { get; set; }
        public List<TransactionPositionsDto> Positions { get; set; }
    }
}