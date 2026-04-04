using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Tutorial.Data;
using Web_API_Tutorial.Models;

namespace Web_API_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupOperatorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GroupOperatorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("groupBy")]
        public async Task<IActionResult>GetBooksSummery()
        {
            var data = await _context.Books
                .GroupBy(b => b.Author.Name)
                .Select(g => new BookSummaryDto
                {
                    AuthorName = g.Key,
                    TotalBooks = g.Count(),
                    TotalPrice = g.Sum(x => x.Price),
                    AvgPrice = g.Average(x => x.Price),
                    MaxPrice = g.Max(x => x.Price),
                    MinPrice = g.Min(x => x.Price)

                }).ToListAsync();
            return Ok(data);
        }

        [HttpGet("availableSummery")]
        public async Task<IActionResult>GetAvailableBooksSummery()
        {
            var data = await _context.Books
                .Where(b => b.IsAvailable)
                .GroupBy(b => b.Author.Name)
                .Select(g => new
                {
                    AuthorName = g.Key,
                    TotalBooks = g.Count(),
                    AvgPrice = g.Average(x => x.Price)

                }).ToListAsync();

            return Ok(data);
        }
    }
}
