using AutoMapper;
using Tryitter.DataContract.Response;
using Tryitter.Models;

namespace Tryitter.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<UserRequest, User>();
        CreateMap<PostRequest, Post>();
        CreateMap<User, UserResponse>();
        CreateMap<Post, PostResponse>();
    }
}