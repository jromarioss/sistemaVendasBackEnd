﻿namespace StoreApi.DTO.User;

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
