using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
using Tryitter.Services;

namespace Tryitter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _postService.GetPostById(id);
        return Ok(post);
    }
    
    [HttpGet("ByUserId{userId:int}")]
    public async Task<IActionResult> GetAllPostsByUserId(int userId)
    {
        var posts = await _postService.GetAllPostsByUserId(userId);

        return Ok(posts);
    }
    
    [HttpGet("LastByUserId{userId:int}")]
    public async Task<IActionResult> GetPostLastByUserId(int userId)
    {
        var post = await _postService.GetPostLastByUserId(userId);

        return Ok(post);
    }
    
    [HttpPost("{userId:int}")]
    public async Task<IActionResult> CreatePost(int userId, [FromBody] PostRequest postRequest)
    {
        var postCreated = await _postService.CreatePost(userId, postRequest);
        return Ok(postCreated);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] PostRequest postRequest)
    {
        var postUpdated = await _postService.UpdatePost(id, postRequest);
        return Ok(postUpdated);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var postToDelete = await _postService.DeletePost(id);
        return Ok(postToDelete);
    }
}