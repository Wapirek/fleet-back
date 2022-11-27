namespace Fleet.Core.Dtos
{
    public class ProductDto
    {
        public int AccountId { get; set; }
        public string ProductName { get; set; }
        public string Place { get; set; }
        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public double Paid { get; set; }
    }
}