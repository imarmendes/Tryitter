using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tryitter.Models;

namespace Tryitter.Repositories;

public class PostRepository : IPostRepository
{
    private readonly TryitterContext _context;

    public PostRepository(TryitterContext context)
    {
        _context = context;
    }
    
    public async Task<Post?> GetPostById(int id)
    {
        try
        {
            var post = _context.Posts.Find(id);
            return post;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar um post por id.");
        }
    }

    public async Task<List<Post>?> GetAllPostsByUserId(int id)
    {
        try
        {
            var user = _context.Users.Find(id);
            var posts = user?.Posts;
            return posts;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar todos os posts de um usuário pelo userId.");
        }
    }

    public async Task<Post?> GetPostLastByUserId(int id)
    {
        try
        {
            var user = _context.Users.Find(id);

            var post = user?.Posts.LastOrDefault();
            return post;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar o útimo post de um usuário pelo userId.");
        }
    }

    public async Task<Post> CreatePost(int userId, Post post)
    {
        try
        {
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            var postCreated = _context.Posts.Add(post).Entity;
            _context.Users.Find(userId).Posts.Add(postCreated);
            _context.SaveChanges();
            
            return postCreated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar criar um post.");
        }
    }

    public async Task<Post> UpdatePost(int id, Post post)
    {
        try
        {
            var postUpdated = _context.Posts.Find(id);
            postUpdated.Message = post.Message;
            postUpdated.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            
            return postUpdated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar atualizar um post.");
        }
    }

    public async Task<Post> DeletePost(Post post)
    {
        try
        {
            var postDeleted = _context.Posts.Remove(post).Entity;
            _context.SaveChanges();
            return postDeleted;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar deletar um post.");
        }
    }
}