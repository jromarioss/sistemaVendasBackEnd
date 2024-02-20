using Microsoft.EntityFrameworkCore;
using StoreApi.Context;
using StoreApi.DTO;
using StoreApi.Model;

namespace StoreApi.Repository;

public class AuthRepository
{
    private readonly AppDbContext _context;
    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> Authenticate(AuthDTO data)
    {
        var user = await _context.Users.FirstOrDefaultAsync(p => p.Email == data.Email);
        return user;
    }
}
