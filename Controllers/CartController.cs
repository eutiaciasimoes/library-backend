using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✔ Cart requires login
    public class CartController : ControllerBase
    {
        private readonly LibraryContext _context;

        public CartController(LibraryContext context)
        {
            _context = context;
        }

        // ⭐ Helper: Get logged-in user ID from JWT
        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        // ⭐ ADD TO CART (User cannot add for someone else)
        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            item.UserId = GetUserId(); // ✔ Force correct user

            _context.CartItems.Add(item);
            _context.SaveChanges();

            return Ok(item);
        }

        // ⭐ GET CART FOR LOGGED-IN USER
        [HttpGet]
        public IActionResult GetCart()
        {
            int userId = GetUserId();

            var items = _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();

            return Ok(items);
        }

        // ⭐ REMOVE SINGLE ITEM (only your own items)
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            int userId = GetUserId();

            var item = _context.CartItems.FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (item == null)
                return NotFound("Item not found in your cart.");

            _context.CartItems.Remove(item);
            _context.SaveChanges();

            return Ok(new { message = "Item removed" });
        }

        // ⭐ CLEAR CART FOR LOGGED-IN USER
        [HttpDelete("clear")]
        public IActionResult ClearCart()
        {
            int userId = GetUserId();

            var items = _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();

            if (!items.Any())
                return NotFound("Your cart is already empty.");

            _context.CartItems.RemoveRange(items);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
