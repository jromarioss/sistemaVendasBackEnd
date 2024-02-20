using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApi.Model;

[Table("Categories")]
public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
        CreatedAt = DateTime.Now;
        Status = true;
    }

    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(250)]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public bool Status { get; set; }

    [JsonIgnore]
    public ICollection<Product> Products { get; set; }
}
