﻿namespace Auth.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public double RegisterCode { get; set; }
}
