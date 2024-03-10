namespace StoreApi.DTO.Product;

public class ProductDTO
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
    public string ImageUrl { get; set; }
    public string Image { get; set; }
}
