﻿namespace WebAppHealthChecker.Application.Authentication.Queries.Login;

public class LoginQuery : IRequest<UserDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}