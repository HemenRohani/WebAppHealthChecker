namespace WebAppHealthChecker.Application.Authentication.Queries.Login;

public class UserDto
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}