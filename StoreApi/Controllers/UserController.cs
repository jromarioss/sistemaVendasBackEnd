using Microsoft.AspNetCore.Mvc;
using StoreApi.DTO;
using StoreApi.Model;
using StoreApi.Repository;

namespace StoreApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #region GET
    [HttpGet("getAllUser")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.ReturnAllUsers();

            if (!users.Any())
            {
                return NotFound(new { message = "Nenhum usuário(a) encontrado." });
            }

            return Ok(users);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getUserById/{id:int}")]
    public async Task<ActionResult<UserReturnDTO>> GetUserId(int id)
    {
        try
        {
            var user = await _userRepository.ReturnUserById(id);

            if (user == null)
            {
                return NotFound(new { message = "Usuário(a) não encontrado." });
            }

            return Ok(user);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getUserInfoById/{id:int}")]
    public async Task<ActionResult<UserInfoReturnDTO>> GetUserInfoId(int id)
    {
        try
        {
            var user = await _userRepository.ReturnUserInfoById(id);

            if (user == null)
            {
                return NotFound(new { message = "Usuário(a) não encontrado." });
            }

            return Ok(user);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getUserAllInfoById/{id:int}")]
    public async Task<ActionResult<UserInfoReturnDTO>> GetUserAllInfoId(int id)
    {
        try
        {
            var user = await _userRepository.ReturnUserAllInfoById(id);

            if (user == null)
            {
                return NotFound(new { message = "Usuário(a) não encontrado." });
            }

            return Ok(user);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getCountUsers")]
    public async Task<ActionResult<int>> GetCountUser()
    {
        try
        {
            int numberOfCategory = await _userRepository.ReturnNumberOfUser();

            if (numberOfCategory < 0)
            {
                return NotFound(new { message = "Usuários não encontrado." });
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

    [HttpGet("getCountEmployee")]
    public async Task<ActionResult<int>> GetCountEmployee()
    {
        try
        {
            int numberOfCategory = await _userRepository.ReturnNumberOfEmployee();

            if (numberOfCategory < 0)
            {
                return NotFound(new { message = "Funcionarios não encontrado." });
            }

            return Ok(numberOfCategory);
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError, new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpGet("getUsersByQuery")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersByQuery(string name, string type)
    {
        try
        {
            var users = await _userRepository.ReturnUsersByQuery(name, type);

            if (users == null)
            {
                return NotFound(new { message = "Usuário(a) não encontrado." });
            }

            return Ok(users);
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError, new { message = "Erro no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region POST
    [HttpPost("createUser")]
    public async Task<ActionResult> CreateUser([FromBody] UserDTO data)
    {
        try
        {
            if (data == null)
            {
                return BadRequest(new { message = "Dados inválido." });
            }

            string userRes = await _userRepository.CreateUser(data);

            if (userRes == "email")
            {
                return Conflict(new { message = "E-mail já existente." });
            }

            if (userRes == "cpf")
            {
                return Conflict(new { message = "CPF já existente." });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = "Usuário criado com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError, new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }
    #endregion

    #region UPDATE
    [HttpPatch("updateUser/{id:int}")]
    public async Task<ActionResult> UserUpdateId(int id, [FromBody] UserUpdateDTO data)
    {
        try
        {
            if (data is null)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            bool userUpdate = await _userRepository.UpdateUser(id, data);

            if (!userUpdate)
            {
                return NotFound(new { message = "Usuário(a) não encontrado." });
            }

            return Ok(new { message = "Usuário(a) atualizado com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    [HttpPatch("updateUserType/{id:int}")]
    public async Task<ActionResult> UserUpdateIdType(int id, [FromBody] int type)
    {
        try
        {
            if (type == null)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            bool userUpdate = await _userRepository.UpdateUserType(id, type);

            if (!userUpdate)
            {
                return NotFound(new { message = "Usuário(a) não encontrado." });
            }

            return Ok(new { message = "Usuário(a) atualizado com sucesso." });
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
    [HttpDelete("deleteUser/{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            bool product = await _userRepository.RemoveUser(id);

            if (!product)
            {
                return NotFound(new { message = "Nenhum usuário(a) foi encontrado." });
            }

            return Ok(new { message = "Usuário(a) excluido com sucesso." });
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
