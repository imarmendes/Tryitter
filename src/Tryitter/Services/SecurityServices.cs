using Tryitter.Models;
using Tryitter.Validation.Base;

namespace Tryitter.Services;

public class SecurityServices : ISecurityServices
{
    public Task<Response<bool>> ComparePassword(string password, string confirmPassword)
    {
        var isEquals = password.Trim().Equals(confirmPassword.Trim());

        return Task.FromResult(Response.Ok<bool>(isEquals));
    }

    public Task<Response<string>> EncryptPassword(string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        return Task.FromResult(Response.Ok<string>(passwordHash));
    }

    public Task<Response<bool>> VerifyPassword(string password, User user)
    {
        bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

        return Task.FromResult(Response.Ok<bool>(validPassword));
    }
}