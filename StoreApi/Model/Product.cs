using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApi.Model;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [StringLength(250)]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    public string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [StringLength(250)]
    public string Size { get; set; }

    [StringLength(250)]
    public string ImagemUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Status { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public Product()
    {
        Status = true;
        CreatedAt = DateTime.Now;
    }
}
