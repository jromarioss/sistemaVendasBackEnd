using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApi.Model;

[Table("DeliveryFee")]
public class DeliveryFee
{
    [Key]
    public int DeliveryFeeId { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Value { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public ICollection<Order> Orders { get; set; }

    public DeliveryFee()
    {
        CreatedAt = DateTime.Now;
    }
}
