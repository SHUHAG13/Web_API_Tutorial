namespace Web_API_Tutorial.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }

        // Navigation Property
        public Author Author { get; set; }
    }
}