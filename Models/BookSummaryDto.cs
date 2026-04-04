namespace Web_API_Tutorial.Models
{
    public class BookSummaryDto
    {
        public string AuthorName { get; set; }
        public int TotalBooks { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
    }
}
