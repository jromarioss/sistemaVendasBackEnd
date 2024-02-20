using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApi.Model;

public enum UserType
{
    Usuario,
    Funcionario,
    Admin
}

[Table("Users")]
public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(250)]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(250, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [StringLength(14, MinimumLength = 14)]
    public string Cpf { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 14)]
    public string Phone { get; set; }

    [Required]
    [StringLength(250)]
    public string Street { get; set; }

    [Required]
    [StringLength(10)]
    public string Number { get; set; }

    [Required]
    [StringLength(250)]
    public string Neighborhood { get; set; }

    [Required]
    [StringLength(250)]
    public string Complement { get; set; }

    [Required]
    [StringLength(250)]
    public string City { get; set; }

    [Required]
    [StringLength(9, MinimumLength = 9)]
    public string Cep { get; set; }

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string State { get; set; }

    public UserType Type { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public ICollection<Order> Orders { get; set; }

    public User()
    {
        CreatedAt = DateTime.Now;
        Orders = new List<Order>();
    }
}
