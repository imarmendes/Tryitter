using Tryitter.Models;

namespace Tryitter.Services;

public interface ITokenGenerator
{
    public string Generate(User user);
}