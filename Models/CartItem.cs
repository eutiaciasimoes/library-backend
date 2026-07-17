using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }   // REQUIRED

        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
