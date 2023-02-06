using Tryitter.Models;

namespace Tryitter.Repositories;

public interface IUserRepository
{
    public Task<bool> UserExist(int userId);
    public Task<List<User>> GetAllUser();
    public Task<User?> GetAllUserById(int id);
    public Task<User?> GetAllUserByName(string name);
    public Task<User?> GetAllUserByEmail(string email);
    public Task<User> CreateUser(User user);
    public Task<User?> UpdateUser(int id, User user);
    public Task<User> DeleteUser(User user);

}