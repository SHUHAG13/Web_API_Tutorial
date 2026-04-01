namespace Web_API_Tutorial.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property (One Author → Many Books)
        public ICollection<Book> Books { get; set; }
    }
}