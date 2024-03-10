using Microsoft.EntityFrameworkCore;
using StoreApi.Context;
using StoreApi.DTO.Product;
using StoreApi.Model;

namespace StoreApi.Repository;

public class ProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    #region GET
    public async Task<IEnumerable<Product>> ReturnAllProducts()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Product>> ReturnAllProductsStatusTrue()
    {
        return await _context.Products.AsNoTracking().Where(p => p.Status == true).ToListAsync();
    }

    public async Task<Product> ReturnProductById(int id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<IEnumerable<Product>> ReturnProductByCategory(int id)
    {
        return await _context.Products.AsNoTracking()
            .Where(p => p.CategoryId == id && p.Status == true)
            .ToListAsync();
    }

    public async Task<int> ReturnNumberOfProduct()
    {
        return await _context.Products.CountAsync();
    }
    #endregion

    #region POST
    public async Task<string> CreateProduct(ProductDTO data)
    {
        bool productAlreadyExist = await _context.Products.AnyAsync(p => p.Name == data.Name);

        if (productAlreadyExist)
        {
            return "exist";
        }

        string imagePath = $@"C:\Users\romar\Documentos\Estudos\Projetos\pizzaria\PizzaFront\public\images";
        string patchImage = Path.Combine(imagePath, data.ImageUrl);

        byte[] convertImage = Convert.FromBase64String(data.Image);
        File.WriteAllBytes(patchImage, convertImage);

        Product product = new ()
        {
            CategoryId = data.CategoryId,
            Name = data.Name,
            Description = data.Description,
            Price = data.Price,
            Size = data.Size,
            ImagemUrl = data.ImageUrl
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return "save";
    }
    #endregion

    #region UPDATE
    public async Task<bool> UpdateProduct(int id, UpdateProductDTO data)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(data.Name))
        {
            product.Name = data.Name;
        }

        if (!string.IsNullOrEmpty(data.ImageUrl))
        {
            string imagePath = $@"C:\Users\romar\Documentos\Estudos\Projetos\pizzaria\PizzaFront\public\images";

            string pathImageToDelete = Path.Combine(imagePath, data.ImageUrlToDelete);

            if (File.Exists(pathImageToDelete))
            {
                File.Delete(pathImageToDelete);
            }

            string pathImageNew = Path.Combine(imagePath, data.ImageUrl);

            byte[] convertImage = Convert.FromBase64String(data.Image);
            File.WriteAllBytes(pathImageNew, convertImage);

            product.ImagemUrl = data.ImageUrl;
        }

        if (!string.IsNullOrEmpty(data.Description))
        {
            product.Description = data.Description;
        }

        if (data.Price != default)
        {
            product.Price = Convert.ToDecimal(data.Price);
        }

        if (!string.IsNullOrEmpty(data.Size))
        {
            product.Size = data.Size;
        }

        if (data.Status != null)
        {
            product.Status = data.Status;
        }

        product.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return true;
    }
    #endregion

    #region DELETE
    public async Task<bool> RemoveProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
    #endregion
}
