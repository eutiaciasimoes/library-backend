using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthController(LibraryContext context)
        {
            _context = context;
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(new
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            });
        }

        // REGISTER
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = "Customer"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            });
        }
    }
}
