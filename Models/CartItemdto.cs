namespace LibraryFrontend.Models
{
	public class CartItemDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public int Quantity { get; set; }
	}
}
