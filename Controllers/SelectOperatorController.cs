using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Tutorial.Data;

namespace Web_API_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectOperatorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SelectOperatorController(AppDbContext context)
        {
            _context = context;
        }

        // SELECT + WHERE → Filter + Projection
        // URL: /api/selectoperator/selectFiltersBooks
        // Returns only required fields (Title, Price, Author Name)
        [HttpGet("selectFiltersBooks")]
        public async Task<IActionResult> GetFilteredSelectedBooks()
        {
            var books = await _context.Books
                .Where(b => b.IsAvailable && b.Price > 700) // filter condition
                .Select(b => new
                {
                    b.Title,
                    b.Price,
                    AuthorName = b.Author.Name // navigation property
                })
                .ToListAsync();

            return Ok(books);
        }

        //  SELECTMANY → Flatten nested collection
        //  Returns all books from all authors as a flat list
        [HttpGet("selectManyBooks")]
        public async Task<IActionResult> GetAllBooksFromAuthors()
        {
            var books = await _context.Authors
                .SelectMany(a => a.Books) // flatten Author → Books
                .ToListAsync();

            return Ok(books);
        }

        // URL: /api/selectoperator/allAuthorBookTitles
        [HttpGet("allAuthorBookTitles")]
        public async Task<IActionResult> GetAllAuthorBookTitles()
        {
            var titles = await _context.Authors
                .SelectMany(a => a.Books) // flatten all books from each author
                .Select(b => b.Title) // then pick the Title
                .ToListAsync();

            return Ok(titles);
        }
    }
}