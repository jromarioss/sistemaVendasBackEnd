using StoreApi.Model;

namespace StoreApi.DTO.User;

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
