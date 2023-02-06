using FluentValidation;
using Tryitter.Models;

namespace Tryitter.Validation;

public class PostValidate : AbstractValidator<PostRequest>
{
    public PostValidate()
    {
        RuleFor(p => p.Message)
            .NotEmpty()
            .WithMessage("A mensagem é obrigatória")
            .MaximumLength(300)
            .WithMessage("O tamanho máximo para mensagens é 300 caracteres.");

        RuleFor(p => p.UserId)
            .NotEmpty()
            .WithMessage("O Id do usuário é obrigatório.");
    }
}