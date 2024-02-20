using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApi.Model;

public enum Payment_type
{
    Pix,
    Dinheiro,
    Credito,
    Debito
}

[Table("Orders")]
public class Order
{
    [Key]
    public int OrderId { get; set; }

    [StringLength(250)]
    public string Description { get; set; }

    public Payment_type PaymentType { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }

    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int DeliveryFeeId { get; set; }
    public DeliveryFee DeliveryFee { get; set; }

    public bool Done { get; set; }

    [StringLength(250)]
    public string ProductIds { get; set; }

    public Order()
    {
        CreatedAt = DateTime.Now;
        Done = false;
    }
}
