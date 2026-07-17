using System.ComponentModel.DataAnnotations;
public class CartItem
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
}
