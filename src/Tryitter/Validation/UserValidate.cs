using FluentValidation;
using Tryitter.Models;

namespace Tryitter.Validation;

public class UserValidate : AbstractValidator<UserRequest>
{
    public UserValidate()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MinimumLength(10)
            .WithMessage("O nome tem que ter o tamanho minimo de 10 caracteres.");

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("O e-mail deve seguir o padrão email@email.com");

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("O senha é obrigatório.")
            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres.");
        
        RuleFor(p => p.ConfirmPassword)
            .NotEmpty()
            .WithMessage("O senha é obrigatório.")
            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres.");
        
        RuleFor(p => p.Module)
            .NotEmpty()
            .WithMessage("O módulo é obrigatório.");
    }
}