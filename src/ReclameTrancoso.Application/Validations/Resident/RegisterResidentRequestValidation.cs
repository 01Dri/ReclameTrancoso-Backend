using Domain.Interfaces;
using Domain.Models.DTOs.Resident;
using FluentValidation;

namespace Application.Validations.Resident;

public class RegisterResidentRequestValidation : AbstractValidator<ResidentRegisterRequestDTO>
{
    private readonly IBuildingRepository _buildingRepository;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IResidentRepository _residentRepository;

    public RegisterResidentRequestValidation
    (
        IBuildingRepository buildingRepository,
        IApartmentRepository apartmentRepository,
        IResidentRepository residentRepository
    )
    {
        _apartmentRepository = apartmentRepository;
        _buildingRepository = buildingRepository;
        _residentRepository = residentRepository;
        
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio.")
            .MinimumLength(5).WithMessage("Nome deve ter no minimo 5 caracteres.")
            .MaximumLength(50).WithMessage("Nome deve ter no maximo 50 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email não pode ser vazio.")
            .EmailAddress().WithMessage("Email deve ser válido.")
            .MustAsync(async (email, _) => !await this._residentRepository.AnyByEmail(email))
            .WithMessage("Email já cadastrado");

        RuleFor(x => x.Cpf)
            .Length(14).WithMessage("CPF deve ter 14 caracteres, Ex: 130.482.459-44")
            .MustAsync(async (cpf, _) => !await this._residentRepository.AnyByCPF(cpf))
            .WithMessage("CPF já cadastrado");

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