using System.Data;
using Domain.Interfaces;
using Domain.Models.DTOs.Resident;
using FluentValidation;

namespace Application.Validations.Resident;

public class ResidentRegisterRequestValidation : AbstractValidator<ResidentRegisterRequestDTO>
{
    private readonly IBuildingRepository _buildingRepository;
    private readonly IApartmentRepository _apartmentRepository;

    public ResidentRegisterRequestValidation(IBuildingRepository buildingRepository, IApartmentRepository apartmentRepository)
    {
        _apartmentRepository = apartmentRepository;
        _buildingRepository = buildingRepository;
        
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio.")
            .MinimumLength(5).WithMessage("Nome deve ter no minimo 5 caracteres.")
            .MaximumLength(50).WithMessage("Nome deve ter no maximo 50 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email não pode ser vazio.")
            .EmailAddress().WithMessage("Email deve ser válido.");

        RuleFor(x => x.Cpf)
            .Length(14).WithMessage("CPF deve ter 14 caracteres, Ex: 130.482.459-44");

        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres")
            .Matches(@"[A-Z]").WithMessage("Senha deve conter pelo menos uma letra maiúscula");

        RuleFor(x => x.BuildingId)
            .MustAsync(async (x, _) => await this._buildingRepository.ExistsByIdAsync(x))
            .WithMessage("Bloco não existe");
        
        RuleFor(x => x.ApartmentId)
            .MustAsync(async (x, _) => await this._apartmentRepository.ExistsByIdAsync(x))
            .WithMessage("Apartamento não existe");

    }
}