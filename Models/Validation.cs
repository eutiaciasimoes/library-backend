using System.ComponentModel.DataAnnotations;
public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    public string Category { get; set; }

    [Range(0, 999)]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}
