using Backend.Models;

namespace LibraryFrontend.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
