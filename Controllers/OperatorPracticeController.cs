using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Tutorial.Data;

namespace Web_API_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorPracticeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OperatorPracticeController(AppDbContext context)
        {
            _context = context;
        }

        // 📌 WHERE → Filter books by availability & price
        // URL: /api/operatorpractice/filterByPrice
        [HttpGet("filterByPrice")]
        public async Task<IActionResult> FilterByPrice()
        {
            var filteredBooks = await _context.Books
                .Where(b => b.IsAvailable && b.Price > 700) // filter condition
                .ToListAsync();

            return Ok(filteredBooks);
        }

        // 📌 DISTINCT → Get unique authors (remove duplicates)
        // URL: /api/operatorpractice/uniqueAuthors
        [HttpGet("uniqueAuthors")]
        public async Task<IActionResult> GetUniqueAuthors()
        {
            var authors = await _context.Books
                .Select(b => b.Author) // select author field
                .Distinct()            // remove duplicate authors
                .ToListAsync();

            return Ok(authors);
        }

        // 📌 SKIP + TAKE → Pagination (page-wise data)
        // URL: /api/operatorpractice/pagination?page=1&pageSize=3
        [HttpGet("pagination")]
        public async Task<IActionResult> PaginationBookList(int page = 1, int pageSize = 3)
        {
            var books = await _context.Books
                .OrderBy(b => b.Id)                       // required for consistent pagination
                .Skip((page - 1) * pageSize)              // skip previous records
                .Take(pageSize)                           // take current page records
                .ToListAsync();

            return Ok(books);
        }

        // 📌 ANY → Check if any book matches condition
        // URL: /api/operatorpractice/any
        [HttpGet("any")]
        public async Task<IActionResult> AnyAsync()
        {
            var booksExist = await _context.Books
                .AnyAsync(b => b.Price > 1000); // returns true/false

            return Ok(booksExist);
        }

        // 📌 ALL → Check if all books match condition
        // URL: /api/operatorpractice/all
        [HttpGet("all")]
        public async Task<IActionResult> AllAsync()
        {
            var allAvailable = await _context.Books
                .AllAsync(b => b.IsAvailable); // সব book available কিনা

            return Ok(allAvailable);
        }

        // 📌 CONTAINS → Filter books by multiple IDs (IN query)
        // URL: /api/operatorpractice/contains?ids=1&ids=2&ids=3
        [HttpGet("contains")]
        public async Task<IActionResult> ContainsAsync([FromQuery] List<int> ids)
        {
            var books = await _context.Books
                .Where(b => ids.Contains(b.Id)) // SQL: WHERE Id IN (...)
                .ToListAsync();

            return Ok(books);
        }
    }
}