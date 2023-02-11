using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TryitterContext _context;

    public UserRepository(TryitterContext context)
    {
        _context = context;
    }


    public async Task<bool> UserExist(int userId)
    {
        var response = _context.Users.Find(userId);
        if (response != null) return true;
        return false;
    }
    public async Task<List<User>> GetAllUser()
    {
        try
        {
            var usersList = _context.Users.ToList();
            return usersList;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar lista de usuário.");
        }
    }

    public async Task<User?> GetAllUserById(int id)
    {
        try
        {
            var response = _context.Users.Find(id);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar um usuário por id.");
        }
        
    }

    public async Task<User?> GetAllUserByName(string name)
    {
        try
        {
            var user = _context.Users.Where(u => u.Name == name).FirstOrDefault();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar um usuário por nome.");
        }
    }
    
    public async Task<User?> GetAllUserByEmail(string email)
    {
        try
        {
            var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar um usuário por e-mail.");
        }
    }

    public async Task<User> CreateUser(User user)
    {
        try
        {
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            var response = _context.Users.Add(user);
            _context.SaveChanges();
            
            return response.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar criar um usuário.");
        }
    }

    public async Task<User> UpdateUser(int id, User user)
    {
        try
        {
            user.Id = id;
            user.UpdatedAt = DateTime.Now;
            var response = _context.Users.Update(user);
            _context.SaveChanges();

            return response.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar atualizar um usuário.");
        }
    }

    public async Task<User> DeleteUser(User user)
    {
        try
        {
            var response = _context.Users.Remove(user);
            _context.SaveChanges();

            return response.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar deletar um usuário.");
        }
    }
}