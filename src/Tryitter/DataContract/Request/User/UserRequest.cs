namespace Tryitter.Models;

public class UserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Module { get; set; }
    public string Status { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}