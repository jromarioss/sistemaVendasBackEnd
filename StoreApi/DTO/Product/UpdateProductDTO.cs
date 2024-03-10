namespace StoreApi.DTO.Product;

public class UpdateProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
    public bool Status { get; set; }
    public string ImageUrl { get; set; }
    public string ImageUrlToDelete { get; set; }
    public string Image { get; set; }
}
