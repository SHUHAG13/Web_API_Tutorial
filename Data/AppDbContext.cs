using Microsoft.EntityFrameworkCore;
using Web_API_Tutorial.Models;

namespace Web_API_Tutorial.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Rahim" },
                new Author { Id = 2, Name = "Karim" },
                new Author { Id = 3, Name = "Jhon" }
            );

            // 🔹 Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "C# Basics", Price = 500, IsAvailable = true, AuthorId = 1 },
                new Book { Id = 2, Title = "ASP.NET Core", Price = 800, IsAvailable = true, AuthorId = 1 },
                new Book { Id = 3, Title = "SQL Guide", Price = 300, IsAvailable = true, AuthorId = 2 },
                new Book { Id = 4, Title = "EF Core Deep Dive", Price = 900, IsAvailable = false, AuthorId = 2 },
                new Book { Id = 5, Title = "LINQ Mastery", Price = 1200, IsAvailable = true, AuthorId = 3 }
            );
        }
    }
}
