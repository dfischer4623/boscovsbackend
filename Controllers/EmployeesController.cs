using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoscovsBackend.Data;
using BoscovsBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace BoscovsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires a valid JWT token to access
    public class EmployeesController : ControllerBase
    {
        private readonly BoscovsDbContext _context;

        public EmployeesController(BoscovsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            // FIX: Match the table (Employees) to the return type (Employee)
            return await _context.Employees.ToListAsync();
        }
    }
}