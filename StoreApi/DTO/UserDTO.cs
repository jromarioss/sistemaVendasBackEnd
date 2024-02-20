using StoreApi.Model;

namespace StoreApi.DTO;

public class UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Cpf { get; set; }
    public string Phone { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string Cep { get; set; }
    public string State { get; set; }
    public UserType? Type { get; set; }
}

public class UserReturnDTO
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class UserInfoReturnDTO
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string Cep { get; set; }
}

public class UserAllInfoDTO
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string Cep { get; set; }
    public string State { get; set; }
}

public class UserUpdateDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string Cep { get; set; }
    public string State { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UserTokenDTO
{
    public string Token { get; set; }
}

public class UserSearchDTO
{
    public string name { get; set; }
    public int type { get; set; }
}
