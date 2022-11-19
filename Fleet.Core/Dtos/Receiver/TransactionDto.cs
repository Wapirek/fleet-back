using System;

namespace Fleet.Core.Dtos
{
    public class TransactionDto
    {
        public double Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Currency { get; set; }
        public double Paid { get; set; }
        public int AccountId { get; set; }
        public int? ProductId { get; set; }
        public int TransactionDirectionId { get; set; }
    }
}