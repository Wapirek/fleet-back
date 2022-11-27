namespace Fleet.Core.Dtos
{
    public class TransactionPositionsDto
    {
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public double Paid { get; set; }
        public string ProductName { get; set; }
    }
}