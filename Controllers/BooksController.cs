using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✔ Must be logged in
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_context.Books.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] //  Only Admin can add books
        public IActionResult AddBook([FromBody] BookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Quantity = dto.Quantity
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok(book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] //  Only Admin can update books
        public IActionResult UpdateBook(int id, [FromBody] BookDto dto)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Quantity = dto.Quantity;

            _context.SaveChanges();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] //  Only Admin can delete books
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok(new { message = "Book deleted" });
        }
    }
}
