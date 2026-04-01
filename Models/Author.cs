namespace Web_API_Tutorial.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalBooks { get; set; }
        public decimal AveragePrice { get; set; }
        public bool HasAvailableBooks { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
