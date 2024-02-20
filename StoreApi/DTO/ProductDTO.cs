namespace StoreApi.DTO;

public class ProductDTO
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
}

public class UpdateProdutDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
    public bool Status { get; set; }
    public DateTime UpdatedAt { get; set; }
}