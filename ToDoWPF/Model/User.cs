using System;
using System.Collections.Generic;

namespace ToDoWPF.Model;

public class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public User() { }

    public User(string login, string pass)
    {
        Login = login;
        Pass = pass;    
    }
}
