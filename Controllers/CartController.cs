using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly LibraryContext _context;

        public CartController(LibraryContext context)
        {
            _context = context;
        }

        // ADD TO CART
        [HttpPost]
        public IActionResult AddToCart(CartItem item)
        {
            _context.CartItems.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }

        // GET CART BY USER
        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            var items = _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();

            return Ok(items);
        }

        // REMOVE SINGLE ITEM
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var item = _context.CartItems.Find(id);
            if (item == null)
                return NotFound();

            _context.CartItems.Remove(item);
            _context.SaveChanges();

            return Ok();
        }

        // ⭐ CLEAR CART FOR USER
        [HttpDelete("clear/{userId}")]
        public IActionResult ClearCart(int userId)
        {
            var items = _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();

            if (!items.Any())
                return NotFound("Cart is already empty.");

            _context.CartItems.RemoveRange(items);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
