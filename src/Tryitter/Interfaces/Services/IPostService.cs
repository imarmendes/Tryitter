using Tryitter.DataContract.Response;
using Tryitter.Models;
using Tryitter.Validation.Base;

namespace Tryitter.Services;

public interface IPostService
{
    public Task<Response> GetPostById(int id);
    public Task<Response> GetAllPostsByUserId(int id);
    public Task<Response> GetPostLastByUserId(int id);
    public Task<Response>CreatePost(int userId, PostRequest postRequest);
    public Task<Response> UpdatePost(int id, PostRequest postRequest);
    public Task<Response> DeletePost(int id);

}