using Domain.Interfaces;
using Domain.Models.DTOs.Resident;
using FluentValidation;
using ReclameTrancoso.Domain.ValueObjects;

namespace Application.Validations.Resident;

public class RegisterResidentRequestValidation : AbstractValidator<ResidentRegisterRequestDTO>
{
    private readonly IBuildingRepository _buildingRepository;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IResidentRepository _residentRepository;
    private readonly IApartmentsResidentsRepository _apartmentsResidentsRepository;

    public RegisterResidentRequestValidation
    (
        IBuildingRepository buildingRepository,
        IApartmentRepository apartmentRepository,
        IResidentRepository residentRepository,
        IApartmentsResidentsRepository apartmentsResidentsRepository
    )
    {
        _apartmentRepository = apartmentRepository;
        _buildingRepository = buildingRepository;
        _residentRepository = residentRepository;
        _apartmentsResidentsRepository = apartmentsResidentsRepository;
        
        
        
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
            .Length(14).WithMessage("CPF deve ter 14 caracteres, Ex: 868.115.090-15")
            .MustAsync(async (cpf, _) => !await this._residentRepository.AnyByCPF(cpf))
            .WithMessage("CPF já cadastrado")
            .Must(cpf => Cpf.IsValid(cpf)).WithMessage("CPF inválido");

        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres")
            .Matches(@"[A-Z]").WithMessage("Senha deve conter pelo menos uma letra maiúscula");

        RuleFor(x => x.BuildingId)
            .NotNull().WithMessage("ID do bloco é obrigatório")
            .MustAsync(async (id, _) => await this._buildingRepository.ExistsByIdAsync(id))
            .WithMessage("Bloco não existe");
        
        RuleFor(x => x.ApartmentId)
            .NotNull().WithMessage("ID do apartamento é obrigatório")
            .MustAsync(async (id, _) => await this._apartmentRepository.ExistsByIdAsync(id))
            .WithMessage("Apartamento não existe");
        
        RuleFor(x => x.ApartmentId)
            .NotNull().WithMessage("ID do apartamento é obrigatório")
            .MustAsync(async (id, _) => !await this._apartmentsResidentsRepository.AlreadyExistOwnerApartment(id))
            .WithMessage("Apartamento já tem um proprietário");
        
        
    }
}