using Tryitter.Models;
using Tryitter.Validation.Base;

namespace Tryitter.Services;

public interface ISecurityServices
{
    public Task<Response<bool>> ComparePassword(string password, string confirmPassword);
    public Task<Response<string>> EncryptPassword(string password);
    public Task<Response<bool>> VerifyPassword(string password, User user);

}