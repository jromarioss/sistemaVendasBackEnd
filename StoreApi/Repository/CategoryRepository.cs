using Microsoft.EntityFrameworkCore;
using StoreApi.Context;
using StoreApi.Model;

namespace StoreApi.Repository;

public class CategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    #region GET
    public async Task<IEnumerable<Category>> ReturnAllCategories()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> ReturnCategoryById(int id)
    {
        return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task<int> ReturnNumberOfCategory()
    {
        return await _context.Categories.CountAsync();
    }
    #endregion

    #region POST
    public async Task<bool> CreateCategory(string name)
    {
        bool categoryAlreadyExist = await _context.Categories.AnyAsync(c => c.Name == name);

        if (categoryAlreadyExist)
        {
            return false;
        }

        Category category = new ()
        {
            Name = name,
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return true;
    }
    #endregion

    #region PUT
    public async Task<bool> UpdateCategory(int id, string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            return false;
        }

        category.Name = name;
        category.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return true;
    }
    #endregion

    #region DELETE
    public async Task<bool> RemoveCategory(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }
    #endregion
}
