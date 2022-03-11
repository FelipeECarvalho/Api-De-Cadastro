﻿namespace CadastroApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Address> Addresses { get; set; }
}

