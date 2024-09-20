using Domain.Interfaces;
using Domain.Models.DTOs.Complaint;
using FluentValidation;

namespace Application.Validations.Complaint;

public class CreateComplaintValidator : AbstractValidator<ComplaintCreateRequestDTO>
{
    private readonly IResidentRepository _residentRepository;

    public CreateComplaintValidator(IResidentRepository residentRepository)
    {
        _residentRepository = residentRepository;

        RuleFor(x => x.ResidentId)
            .NotEmpty().WithMessage("Resident ID é obrigatório.")
            .NotNull().WithMessage("Resident ID é obrigatório.")
            .MustAsync(async (id, _) => await this._residentRepository.ExistsByIdAsync(id))
            .WithMessage("Residente não existe.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titulo é obrigatório.")
            .NotNull().WithMessage("Titulo é obrigatório.")
            .Length(5, 50).WithMessage("Titulo deve ter entre 5 a 50 caracteres.")
            ;

        RuleFor(x => x.Description)
            .Length(5, 255).WithMessage("Descrição deve ter entre 5 a 255 caracteres.")
            .NotEmpty().WithMessage("Descrição é obrigatório.")
            .NotNull().WithMessage("Descrição é obrigatório.");

        RuleFor(x => x.ComplaintType)
            .NotEmpty().WithMessage("Tipo da reclamação é obrigatório.")
            .NotNull().WithMessage("Tipo da reclamação é obrigatório.")
            .IsInEnum().WithMessage("Tipo da reclamação inválida.");

        RuleFor(x => x.IsAnonymous)
            .NotNull().WithMessage("Opção de anonimato é obrigatória.");
    }

}