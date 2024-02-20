using Microsoft.EntityFrameworkCore;
using StoreApi.Context;
using StoreApi.DTO;
using StoreApi.Model;

namespace StoreApi.Repository;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    #region GET
    public async Task<IEnumerable<User>> ReturnAllUsers()
    {//admin
        try
        {
            var users = await _context.Users.AsNoTracking().OrderBy(user => user.Name).ToListAsync();
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserReturnDTO> ReturnUserById(int id)
    {//all
        try
        {
            var user =  await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserId == id);

            UserReturnDTO editUser = new()
            {
                UserId = id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };

            return editUser;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserInfoReturnDTO> ReturnUserInfoById(int id)
    {
        try
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserId == id);

            UserInfoReturnDTO editUser = new()
            {
                UserId = id,
                Name = user.Name,
                Phone = user.Phone,
                Street = user.Street,
                Number = user.Number,
                Neighborhood = user.Neighborhood,
                Complement = user.Complement,
                Cep = user.Cep,
                City = user.City
            };

            return editUser;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserAllInfoDTO> ReturnUserAllInfoById(int id)
    {
        try
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserId == id);

            UserAllInfoDTO editUser = new()
            {
                UserId = id,
                Name = user.Name,
                Email = user.Email,
                Cpf = user.Cpf,
                Phone = user.Phone,
                Password = user.Password,
                Street = user.Street,
                Number = user.Number,
                Neighborhood = user.Neighborhood,
                Complement = user.Complement,
                Cep = user.Cep,
                City = user.City,
                State = user.State
            };

            return editUser;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<User>> ReturnUsersByQuery(string name, string? type)
    {
        try
        {
            IQueryable<User> query = _context.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(user => user.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(user => user.Type == (UserType)Convert.ToInt32(type));
            }

            var users = await query.OrderBy(user => user.Name).ToListAsync();

            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int> ReturnNumberOfUser()
    {//admin
        try
        {
            return await _context.Users.CountAsync(u => u.Type == UserType.Usuario);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int> ReturnNumberOfEmployee()
    {//admin
        try
        {
            return await _context.Users.CountAsync(u => u.Type == UserType.Funcionario);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region POST
    public async Task<string> CreateUser(UserDTO data)
    {
        try
        {
            bool emailAlreadExist = await _context.Users.AnyAsync(email => email.Email == data.Email);

            if (emailAlreadExist)
            {
                return "email";
            }

            bool cpfAlreadExist = await _context.Users.AnyAsync(cpf => cpf.Cpf == data.Cpf);

            if (cpfAlreadExist)
            {
                return "cpf";
            }

            User newUser = new()
            {
                Name = data.Name,
                Email = data.Email,
                Password = data.Password,
                Cpf = data.Cpf,
                Phone = data.Phone,
                Street = data.Street,
                Number = data.Number,
                Neighborhood = data.Neighborhood,
                Complement = data.Complement,
                City = data.City,
                Cep = data.Cep,
                State = data.State,
                Type = data.Type ?? UserType.Usuario
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return "Ok";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region UPDATE
    public async Task<bool> UpdateUser(int id, UserUpdateDTO data)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);

            if (user == null)
            {
                return false;
            }

            if (data.Email != null)
            {
                user.Email = data.Email;
            }

            if (data.Password != null)
            {
                user.Password = data.Password;
            }

            if (data.Phone != null)
            {
                user.Phone = data.Phone;
            }

            if (data.Street != null)
            {
                user.Street = data.Street;
            }

            if (data.Number != null)
            {
                user.Number = data.Number;
            }

            if (data.Neighborhood != null)
            {
                user.Neighborhood = data.Neighborhood;
            }

            if (data.Complement != null)
            {
                user.Complement = data.Complement;
            }

            if (data.City != null)
            {
                user.City = data.City;
            }

            if (data.Cep != null)
            {
                user.Cep = data.Cep;
            }

            if (data.State != null)
            {
                user.State = data.State;
            }

            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UpdateUserType(int id, int type)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);

            if (user == null)
            {
                return false;
            }

            if (Enum.IsDefined(typeof(UserType), type))
            {
                user.Type = (UserType)type;
            }
            else
            {
                throw new ArgumentException("Tipo de usuário inválido.");
            }

            user.UpdatedAt = DateTime.Now;

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
    public async Task<bool> RemoveUser(int id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}
