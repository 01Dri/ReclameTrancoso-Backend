using Domain.Models.DTOs.Auth;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using ReclameTrancoso.Domain.ValueObjects;

namespace Application.Validations.Auth;

public class LoginRequestValidation : AbstractValidator<LoginRequestDTO>
{

    public LoginRequestValidation()
    {
        RuleFor(x => x)
            .Custom((request, context) =>
            {
                if (request.Cpf.IsNullOrEmpty() && request.Email.IsNullOrEmpty())
                {
                    context.AddFailure("Cpf-Email", "CPF ou Email não podem ser vazios.");
                }

                if (!request.Cpf.IsNullOrEmpty() && !request.Email.IsNullOrEmpty())
                {
                    context.AddFailure("Cpf-Email", "Forneça apenas uma forma de login, CPF ou Email");
                }
            });
        
        RuleFor(x => x.Cpf)
            .NotEmpty().When(x => x.Email.IsNullOrEmpty()).WithMessage("CPF não pode ser vazio.")
            .Length(14).When(x => x.Email.IsNullOrEmpty()).WithMessage("CPF deve ter 14 caracteres, Ex: 868.115.090-15")
            .Must(cpf => Cpf.IsValid(cpf)).When(x => x.Email.IsNullOrEmpty()).WithMessage("CPF inválido");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => x.Cpf.IsNullOrEmpty()).WithMessage("Email deve ser válido.");


        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha não pode ser vazia")
            .MinimumLength(2).WithMessage("Senha inválida");
    }
}