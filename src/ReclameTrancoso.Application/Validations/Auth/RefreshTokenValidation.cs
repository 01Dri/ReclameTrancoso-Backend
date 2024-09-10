using Domain.Models.DTOs.Auth;
using FluentValidation;

namespace Application.Validations.Auth;

public class RefreshTokenValidation : AbstractValidator<RefreshTokenRequestDTO>
{
    public RefreshTokenValidation()
    {
        RuleFor(x => x.RefreshToken)
            .NotNull().WithMessage("Refresh Token é obrigatório")
            .NotEmpty().WithMessage("Refresh Token não pode estar vazio")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Refresh Token deve ser um GUID válido");
    }
}