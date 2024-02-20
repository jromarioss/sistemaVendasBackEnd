using Microsoft.EntityFrameworkCore;
using StoreApi.Context;
using StoreApi.DTO;
using StoreApi.Model;

namespace StoreApi.Repository;

public class OrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    #region GET
    public async Task<IEnumerable<Order>> ReturnAllOrders()
    {//admin
        return await _context.Orders.AsNoTracking()
            .Include(o => o.User)
            .Include(o => o.DeliveryFee)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> ReturnOrdersToday()
    {//Funcionario e admin
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);

        return await _context.Orders.AsNoTracking()
            .Where(o => o.CreatedAt >= today && o.CreatedAt < tomorrow)
            .Include(o => o.User)
            .Include(o => o.DeliveryFee)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> ReturnOrdersDone()
    {//admin
        return await _context.Orders.AsNoTracking().Where(o => o.Done == true)
            .OrderBy(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> ReturnOrderByUserId(int id)
    {//User e admin
        return await _context.Orders.AsNoTracking()
            .Where(o => o.UserId == id)
            .Include(o => o.DeliveryFee)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> ReturnNumberOfOrders()
    {
        return await _context.Orders.CountAsync();
    }
    #endregion

    #region POST
    public async Task<bool> CreateOrderByUser(CreateOrderDTO data)
    {
        try
        {
            Order newOrder = new()
            {
                UserId = data.UserId,
                DeliveryFeeId = data.DeliveryFeeId,
                Description = data.Description,
                PaymentType = data.PaymentType,
                Total = data.Total,
                Amount = data.Amount,
                ProductIds = data.ProductIds,
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion

    #region UPDATE
    public async Task<bool> MakeDoneOrder(int id, bool done)
    {
        try
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id && o.Done == false);

            if (order == null)
            {
                return false;
            }

            if (done != null)
            {
                order.Done = done;
            }

            order.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region DELETE
    public async Task<bool> RemoveOrder(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == id);

        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return true;
    }
    #endregion
}
