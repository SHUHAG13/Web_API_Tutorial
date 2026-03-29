using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Tutorial.Data;

namespace Web_API_Tutorial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController:ControllerBase
    {
            private readonly AppDbContext _context;
    
            public BooksController(AppDbContext context)
            {
                _context = context;
            }

        [HttpGet("getById")]
        public async Task<IActionResult> GetAvailableBooks(int id)
        {
            var books = await _context.Books.FirstOrDefaultAsync(b => b.Id==id);
            return Ok(books);
        }

        [HttpGet("allAvailableBooks")]
            public async Task<IActionResult> GetAvailableBooks()
            {
                var books = await _context.Books.Where(b => b.IsAvailable).ToListAsync();
                return Ok(books);
            }

          [HttpGet("filterByPrice")]
          public async Task<IActionResult>FilterByPrice()
           {
                var filteredBooks = await _context.Books.Where(b => b.IsAvailable && b.Price > 700).ToListAsync();
                return Ok(filteredBooks);

           }

        [HttpGet("orderByPrice")]
        public async Task<IActionResult> OrderByPrice()
        {
            var filteredBooks = await _context.Books.OrderByDescending(b=>b.Price).ToListAsync();
            return Ok(filteredBooks);

        }

        [HttpGet("returnSpecificData")]
        public async Task<IActionResult> ReturnSpecificData()
        {
            var books = await _context.Books.Select(b => new
            {
                b.Price,
                b.Author
            }).ToListAsync();
            return Ok(books);

        }


    }
}
