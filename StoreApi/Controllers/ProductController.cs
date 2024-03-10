using Microsoft.AspNetCore.Mvc;
using StoreApi.DTO.Product;
using StoreApi.Model;
using StoreApi.Repository;

namespace StoreApi.Controllers;

//[Authorize(AuthenticationSchemes = "Bearer")]
[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;

    public ProductController(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    #region GET
    [HttpGet("getAllProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        try
        {
            var products = await _productRepository.ReturnAllProducts();

            if (!products.Any())
            {
                return NotFound(new { message = "Nenhum produto foi encontrado." });
            }

            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getAllProductsMenu")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsStatusTrue()
    {
        try
        {
            var products = await _productRepository.ReturnAllProductsStatusTrue();

            if (!products.Any())
            {
                return NotFound(new { message = "Nenhum produto foi encontrado." });
            }

            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getProductById/{id:int}")]
    public async Task<ActionResult<Product>> GetProductId(int id)
    {
        try
        {
            var product = await _productRepository.ReturnProductById(id);

            if (product == null)
            {
                return NotFound(new { message = "Nenhum produto foi encontrado." });
            }

            return Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getProductByCategory/{id:int}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductCategory(int id)
    {
        try
        {
            var products = await _productRepository.ReturnProductByCategory(id);

            if (!products.Any())
            {
                return NotFound(new { message = "Nenhum produto foi encontrado." });
            }

            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getCountProduct")]
    public async Task<ActionResult<int>> GetCountProduct()
    {
        try
        {
            int numberOfProduct = await _productRepository.ReturnNumberOfProduct();

            if (numberOfProduct < 0)
            {
                return NotFound(new { message = "Nenhum produto foi encontrado." });
            }

            return Ok(numberOfProduct);
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region POST
    [HttpPost("createProduct")]
    public async Task<ActionResult> CreateProduct([FromBody] ProductDTO data)
    {
        try
        {
            if (data is null)
            {
                return BadRequest(new { message = "Dados inválido." });
            }

            string productRes = await _productRepository.CreateProduct(data);

            if (productRes == "exist")
            {
                return Conflict(new { message = "Produto já cadastrado." });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = "Produto criado com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region UPDATE
    [HttpPatch("updateProduct/{id:int}")]
    public async Task<ActionResult> UpdateProdutoById(int id, [FromBody] UpdateProductDTO data)
    {
        try
        {
            if (data is null)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            bool productUpdated = await _productRepository.UpdateProduct(id, data);

            if (!productUpdated)
            {
                return NotFound(new { message = "Nenhum produto foi encontrado." });
            }

            return Ok(new { message = "Produto atualizado com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region DELETE
    [HttpDelete("deleteProduct/{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        try
        {
            bool product = await _productRepository.RemoveProduct(id);

            if (!product)
            {
                return NotFound(new { message = "Nenhum produto foi encontrado." });
            }

            return Ok(new { message = "Produto excluido com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion
}
