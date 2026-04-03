using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Web_API_Tutorial.Data;

namespace Web_API_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinController : ControllerBase
    {

        private readonly AppDbContext _context;

        public JoinController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("innerJoin")]
        public async Task<IActionResult> GetInnerJoinData()
        {
            var result = await _context.Students
                .Join(_context.Departments,
                    s => s.DepartmentId, d => d.Id,
                    (s, d) => new
                    {
                        StudentName = s.Name,
                        DepartmentName = d.DepartmentName
                    })
                .ToListAsync();
            return Ok(result);

        }


        [HttpGet("leftJoinOld")]
        public async Task<IActionResult> GetLeftJoinOld()
        {
            var result = await _context.Students
                .GroupJoin( _context.Departments,
                    s => s.DepartmentId,d => d.Id,
                    (s, deptGroup) => new { s, deptGroup }
                )
                .SelectMany(
                    x => x.deptGroup.DefaultIfEmpty(),
                    (x, d) => new
                    {
                        StudentName = x.s.Name,
                        DepartmentName = d != null ? d.DepartmentName : "No Department"
                    }
                )
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("leftJoinNew")]
        public async Task<IActionResult> GetLeftJoinNew()
        {
            var result = await _context.Students
                .LeftJoin(_context.Departments,
                    s => s.DepartmentId,
                    d => d.Id,
                    (s, d) => new
                    {
                        StudentId = s.Id,
                        StudentName = s.Name,
                        DepartmentId = s.DepartmentId,
                        DepartmentName = d != null ? d.DepartmentName : "No Department"
                    }
                )
                .OrderBy(x => x.StudentId)
                .ThenBy(x => x.DepartmentId)
                .ToListAsync();

            return Ok(result);
        }
    }
}
