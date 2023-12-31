﻿namespace WebAppHealthChecker.Application.Authentication.Commands.UserRegister;

public record UserRegisterCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}