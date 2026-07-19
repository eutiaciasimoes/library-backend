using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous] // ✔ Login/Register must NOT require token
    public class AuthController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthController(LibraryContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto, [FromServices] JwtService jwtService)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            if (user.Password != loginDto.Password)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = jwtService.GenerateToken(user.Id, user.Email, user.Role);

            return Ok(new
            {
                token = token,
                user = new
                {
                    id = user.Id,
                    name = user.FullName,
                    email = user.Email,
                    role = user.Role
                }
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password, // later we hash
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully" });
        }
    }
}
