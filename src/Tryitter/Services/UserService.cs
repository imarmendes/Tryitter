using AutoMapper;
using Tryitter.DataContract.Response;
using Tryitter.Models;
using Tryitter.Repositories;
using Tryitter.Validation;
using Tryitter.Validation.Base;

namespace Tryitter.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISecurityServices _securityServices;
    private readonly ITokenGenerator _tokenGenerator;


    public UserService(
        IUserRepository userRepository, 
        IMapper mapper, 
        ISecurityServices securityServices,
        ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _securityServices = securityServices;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Response> GetAuth(AuthRequest authRequest)
    {
        try
        {
            var userToAuth = await _userRepository.GetAllUserByEmail(authRequest.Email);

            var validPassword = await _securityServices.VerifyPassword(authRequest.Password, userToAuth);
            if (!validPassword.Data) 
                return Response.Unprocessable(Report.Create("E-mail ou senha são inválidos."));
            var token = _tokenGenerator.Generate(userToAuth);
            return Response.Ok<string>(token);
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create("E-mail ou senha são inválidos."));
        }
    }
    public async Task<Response> GetAllUser()
    {
        try
        {
            var userList = await _userRepository.GetAllUser();
            var userResponseList = _mapper.Map<List<User>, List<UserResponse>>(userList);
            var response = new Response<List<UserResponse>>(userResponseList);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetAllUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetAllUserById(id);

            var userResponse = _mapper.Map<UserResponse>(user);
            var response = new Response<UserResponse>(userResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetAllUserByName(string name)
    {
        try
        {
            var user = await _userRepository.GetAllUserByName(name);
            var userResponse = _mapper.Map<UserResponse>(user);

            var response = new Response<UserResponse>(userResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> CreateUser(UserRequest userRequest)
    {
        try
        {
            var userValidation = new UserValidate();
            var userIsValid = userValidation.Validate(userRequest);

            var errors = GetValidations.GetErrors(userIsValid);
            
            if (errors.Report.Any())
                return errors;
            
            var isEquals = await _securityServices.ComparePassword(userRequest.Password, userRequest.ConfirmPassword);

            if (!isEquals.Data)
                return Response.Unprocessable(Report.Create("Os password não são iguais."));

            var passwordEncripted = await _securityServices.EncryptPassword(userRequest.Password);

            userRequest.Password = passwordEncripted.Data;

            var user = _mapper.Map<User>(userRequest);
            
            var userCreated = await _userRepository.CreateUser(user);
            var userResponse = _mapper.Map<UserResponse>(userCreated);

            var response = new Response<UserResponse>(userResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }

    }

    public async Task<Response> UpdateUser(int id, UserRequest userRequest)
    {   
        try
        {
            var userValidation = new UserValidate();
            var userIsValid = userValidation.Validate(userRequest);

            var errors = GetValidations.GetErrors(userIsValid);
            
            if (errors.Report.Any())
                return errors;
            
            var user = _mapper.Map<User>(userRequest);

            var userExist = await  _userRepository.UserExist(id);
            if (userExist) return Response.Unprocessable(Report.Create("Usuário a ser atualizado não existe;"));
        
            var userUpdated = await _userRepository.UpdateUser(id, user);
            var userResponse = _mapper.Map<UserResponse>(userUpdated);

            var response = new Response<UserResponse>(userResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> DeleteUser(int id)
    {
        try
        {
            var userExist = await _userRepository.UserExist(id);
            if (userExist) return Response.Unprocessable(Report.Create("Usuário a ser deletado não existe;"));

            var userToDelete = await _userRepository.GetAllUserById(id);
            var userDeleted = await _userRepository.DeleteUser(userToDelete);
            var userResponse = _mapper.Map<UserResponse>(userDeleted);

            var response = new Response<UserResponse>(userResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }
}