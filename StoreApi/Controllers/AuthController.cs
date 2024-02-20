using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreApi.DTO;
using StoreApi.Model;
using StoreApi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthController(
        AuthRepository authRepository,
        IConfiguration configuration
    )
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    [HttpPost("session")]
    public async Task<ActionResult> Auth([FromBody] AuthDTO data)
    {
        try
        {
            if (string.IsNullOrEmpty(data.Email))
            {
                return BadRequest(new { message = "Informe o seu e-mail." });
            }

            if (string.IsNullOrEmpty(data.Password))
            {
                return BadRequest(new { message = "Informe a sua senha." });
            }

            var user = await _authRepository.Authenticate(data);

            if (user == null || user.Password != data.Password)
            {
                return BadRequest(new { message = "E-mail ou senha inválidos." });
            }

            return Ok(GeraToken(user.UserId, user.Type));
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    new { message = "Error no servidor, tente novamente mais tarde." });
        }
    }

    private UserTokenDTO GeraToken(int userId, UserType userType)
    {
        //define declarações do usuário
        var claims = new[]
        {
            new Claim("userId", userId.ToString()),
            new Claim("userType", userType.ToString()),
        };

        //gera uma chave com base em um algoritmo simetrico
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
        //gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //Tempo de expiracão do token.
        var expiracao = _configuration["TokenConfig:ExpireHours"];
        var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

        // classe que representa um token JWT e gera o token
        JwtSecurityToken token = new JwtSecurityToken(
          claims: claims,
          expires: expiration,
          signingCredentials: credenciais);

        //retorna os dados com o token e informacoes
        return new UserTokenDTO()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }
}
