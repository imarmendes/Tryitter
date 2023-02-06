using Tryitter.DataContract.Response;
using Tryitter.Models;
using Tryitter.Validation.Base;

namespace Tryitter.Services;

public interface IUserService
{
    public Task<Response> GetAuth(AuthRequest authRequest);
    public Task<Response> GetAllUser();
    public Task<Response> GetAllUserById(int id);
    public Task<Response> GetAllUserByName(string name);
    public Task<Response> CreateUser(UserRequest userRequest);
    public Task<Response> UpdateUser(int id, UserRequest userRequest);
    public Task<Response> DeleteUser(int id);


}