using Microsoft.EntityFrameworkCore;
using StoreApi.Context;
using StoreApi.Model;

namespace StoreApi.Repository;

public class DeliveryFeeRepository
{
    private readonly AppDbContext _context;
    public DeliveryFeeRepository(AppDbContext context)
    {
        _context = context;
    }

    #region GET
    public async Task<DeliveryFee> ReturnDeliveryFee()
    {
        var taxa = await _context.DeliveryFees.AsNoTracking().FirstOrDefaultAsync(d => d.Value > 0);
        return taxa;
    }
    #endregion

    #region POST
    public async Task<string> CreateDeliveryFee(decimal value)
    {
        bool priceAlreadyExist = await _context.DeliveryFees.AnyAsync(d => d.Value == value);

        if (priceAlreadyExist)
        {
            return "exist";
        }

        int justOneFee = await _context.DeliveryFees.CountAsync();

        if (justOneFee > 0)
        {
            return "limit";
        }

        DeliveryFee newFee = new()
        {
            Value = value,
        };

        _context.DeliveryFees.Add(newFee);
        await _context.SaveChangesAsync();

        return "save";
    }
    #endregion

    #region UPDATE
    public async Task<bool> UpdateDeliveryFee(int id, decimal value)
    {
        var taxa = await _context.DeliveryFees.FirstOrDefaultAsync(d => d.DeliveryFeeId ==id);

        if (taxa == null)
        {
            return false;
        }

        taxa.Value = value;
        taxa.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return true;
    }
    #endregion
}
