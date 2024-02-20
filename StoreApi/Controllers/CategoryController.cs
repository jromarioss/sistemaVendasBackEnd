using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Model;
using StoreApi.Repository;

namespace StoreApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryController(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    #region GET
    [HttpGet("getAllCategory")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        try
        {
            var categories = await _categoryRepository.ReturnAllCategories();

            if (!categories.Any())
            {
                return NotFound(new { message = "Nenhuma categoria encontrada." });
            }

            return Ok(categories);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getCategoryById/{id:int}")]
    public async Task<ActionResult<Category>> GetCategoryId(int id)
    {
        try
        {
            var category = await _categoryRepository.ReturnCategoryById(id);

            if (category == null)
            {
                return NotFound(new { message = "Categoria não encontrada." });
            }

            return Ok(category);
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
                new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getCountCategory")]
    public async Task<ActionResult<int>> GetCountCategory()
    {
        try
        {
            int numberOfCategory = await _categoryRepository.ReturnNumberOfCategory();

            if (numberOfCategory < 0)
            {
                return NotFound(new { message = "Categoria não encontrada." });
            }

            return Ok(numberOfCategory);
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
    [HttpPost("createCategory")]
    public async Task<ActionResult> CreateCategory([FromBody] string name)
    {
        try
        {
            if (name == "")
            {
                return BadRequest(new { message = "Dados inválido." });
            }

            bool categoryRes = await _categoryRepository.CreateCategory(name);

            if (!categoryRes)
            {
                return Conflict(new { message = "Categoria já cadastrada." });
            }

            return StatusCode(StatusCodes.Status201Created,
                new { message = "Categoria criado com sucesso." });
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
    [HttpPatch("updateCategory/{id:int}")]
    public async Task<ActionResult> UpdateCategory(int id, [FromBody] string name)
    {
        try
        {
            if (name == "")
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            bool category = await _categoryRepository.UpdateCategory(id, name);

            if (!category)
            {
                return NotFound(new { message = "Categoria não encontrada." });
            }

            return Ok(new { message = "Categoria atualizada com sucesso." });
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
    [HttpDelete("deleteCategory/{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        try
        {
            bool category = await _categoryRepository.RemoveCategory(id);

            if (!category)
            {
                return NotFound("Categoria não encontrada.");
            }

            return Ok(new { message = "Categoria excluida com sucesso." });
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
