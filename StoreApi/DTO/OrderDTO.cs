using StoreApi.Model;

namespace StoreApi.DTO;

public class OrderDTO
{
    public int UserId { get; set; }
    public int DeliveryFeeId { get; set; }
    public string Description { get; set; }
    public Payment_type PaymentType { get; set; }
    public decimal Total { get; set; }
    public int Amount { get; set; }
}

public class CreateOrderDTO
{
    public int UserId { get; set; }
    public int DeliveryFeeId { get; set; }
    public string Description { get; set; }
    public Payment_type PaymentType { get; set; }
    public decimal Total { get; set; }
    public int Amount { get; set; }
    public string ProductIds { get; set; }
}
