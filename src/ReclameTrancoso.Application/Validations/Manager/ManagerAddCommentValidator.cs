using Domain.Interfaces;
using Domain.Models.DTOs.Manager;
using FluentValidation;

namespace Application.Validations.Manager;

public class ManagerAddCommentValidator : AbstractValidator<ManagerAddCommentRequestDTO>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IManagerRepository _managerRepository;

    public ManagerAddCommentValidator(IComplaintRepository complaintRepository, IManagerRepository managerRepository)
    {
        _complaintRepository = complaintRepository;
        _managerRepository = managerRepository;

        RuleFor(x => x.ManagerId)
            .NotEmpty().WithMessage("Manager ID é obrigatório.")
            .NotNull().WithMessage("Manager ID é obrigatório.")
            .MustAsync(async (id, _) =>  await _managerRepository.ExistsByIdAsync(id)).WithMessage("Manager não existe.");


        RuleFor(x => x.ComplaintId)
            .NotEmpty().WithMessage("Complaint ID é obrigatório.")
            .NotNull().WithMessage("Complaint ID é obrigatório.")
            .MustAsync(async (id, _) =>  await _complaintRepository.ExistsByIdAsync(id)).WithMessage("Manager não existe.");


        RuleFor(x => x.Comment)
            .NotNull().WithMessage("Comment é obrigatório.")
            .NotEmpty().WithMessage("Comment é obrigatório.")
            .Length(5, 255).WithMessage("Comentário deve ter entre 5 a 255 caracteres.");

    }
}