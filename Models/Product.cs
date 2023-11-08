namespace Kit19.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public DateTime MfgDate { get; set; }
        public string Category { get; set; }
    }

    public class ProductSearchViewModel
    {
        public string ProductName { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public DateTime? MfgDate { get; set; }
        public string Category { get; set; }
        public string Conjunction { get; set; } // AND or OR
        public List<Product> SearchResults { get; set; }
    }
}
