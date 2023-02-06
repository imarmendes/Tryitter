using Tryitter.Models;

namespace Tryitter.Repositories;

public interface IPostRepository
{
    public Task<Post?> GetPostById(int id);
    public Task<List<Post>?> GetAllPostsByUserId(int id);
    public Task<Post?> GetPostLastByUserId(int id);
    public Task<Post> CreatePost(int userId, Post post);
    public Task<Post> UpdatePost(int id, Post post);
    public Task<Post> DeletePost(Post post);
}