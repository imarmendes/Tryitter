using AutoMapper;
using Tryitter.DataContract.Response;
using Tryitter.Models;
using Tryitter.Repositories;
using Tryitter.Validation;
using Tryitter.Validation.Base;

namespace Tryitter.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<Response>GetPostById(int id)
    {
        try
        {
            var post = await _postRepository.GetPostById(id);
            var postResponse = _mapper.Map<PostResponse>(post);
            var response = new Response<PostResponse>(postResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }


    public async Task<Response> GetAllPostsByUserId(int id)
    {
        try
        {
            var postList = await _postRepository.GetAllPostsByUserId(id);
            var postResponseList = _mapper.Map<List<Post>, List<PostResponse>>(postList);

            var response = new Response<List<PostResponse>>(postResponseList);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetPostLastByUserId(int id)
    {
        try
        {
            var lastPost = await _postRepository.GetPostLastByUserId(id);
            var postResponse = _mapper.Map<PostResponse>(lastPost);
            var response = new Response<PostResponse>(postResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }

    }

    public async Task<Response> CreatePost(int userId, PostRequest postRequest)
    {
        try
        {
            var postValidation = new PostValidate();
            var postIsValid = postValidation.Validate(postRequest);

            var errors = GetValidations.GetErrors(postIsValid);
            
            if (errors.Report.Any())
                return errors;
            
            var post = _mapper.Map<Post>(postRequest);

            var postCreated = await _postRepository.CreatePost(userId, post);
            var postResponse = _mapper.Map<PostResponse>(postCreated);

            var response = new Response<PostResponse>(postResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> UpdatePost(int id, PostRequest postRequest)
    {
        try
        {
            var postValidation = new PostValidate();
            var postIsValid = postValidation.Validate(postRequest);

            var errors = GetValidations.GetErrors(postIsValid);
            
            if (errors.Report.Any())
                return errors;

            var postToUpdate = await _postRepository.GetPostById(id);
            if (postToUpdate == null) return Response.Unprocessable(Report.Create("Post a ser atualizado não existe;"));

            var post = _mapper.Map<Post>(postRequest);

            var postUpdated = await _postRepository.UpdatePost(id, post);
            var postResponse = _mapper.Map<PostResponse>(postUpdated);

            var response = new Response<PostResponse>(postResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> DeletePost(int id)
    {
        try
        {
            var postToDelete = await _postRepository.GetPostById(id);
            if (postToDelete == null) return Response.Unprocessable(Report.Create("Post a ser deletado não existe;"));

            
            await _postRepository.DeletePost(postToDelete);
            var postResponse = _mapper.Map<PostResponse>(postToDelete);

            var response = new Response<PostResponse>(postResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }
}