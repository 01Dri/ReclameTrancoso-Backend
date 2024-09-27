using Application.Mappers;
using Domain.Interfaces;
using Domain.Models.DTOs.Manager;
using FluentValidation;
using ReclameTrancoso.Domain.Enums;
using ReclameTrancoso.Domain.Interfaces.Transactions;
using ReclameTrancoso.Domain.Models;

namespace Application.UseCases.Manager;

public class ManagerAddCommentUseCase : IUseCaseHandler<ManagerAddCommentRequestDTO, ManagerAddCommentResponseDTO>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IManagerComplaintCommentsRepository _managerComplaintCommentsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IComplaintRepository _complaintRepository;
    private readonly IValidator<ManagerAddCommentRequestDTO> _validator;

    public ManagerAddCommentUseCase(ICommentRepository commentRepository, IManagerComplaintCommentsRepository managerComplaintCommentsRepository, IUnitOfWork unitOfWork, IComplaintRepository complaintRepository, IValidator<ManagerAddCommentRequestDTO> validator)
    {
        _commentRepository = commentRepository;
        _managerComplaintCommentsRepository = managerComplaintCommentsRepository;
        _unitOfWork = unitOfWork;
        _complaintRepository = complaintRepository;
        _validator = validator;
    }



    public async Task<ManagerAddCommentResponseDTO> Handle(ManagerAddCommentRequestDTO request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        _unitOfWork.Begin();
        try
        {
            var entity = request.ToCommentEntity();
            await _commentRepository.SaveAsync(entity);


            var managerComment = await _managerComplaintCommentsRepository.GetByCommentIdAsync(entity.Id);
            if (managerComment == null)
            {
                managerComment = request.ToEntity();
            }
            
            managerComment.CommentId = entity.Id;
            await _managerComplaintCommentsRepository.SaveAsync(managerComment);

            var complaint = await _complaintRepository.GetByIdAsync(request.ComplaintId);
            complaint.Status = complaint.Status == 
                               ComplaintStatus.NO_TREATMENT
                ? ComplaintStatus.TREATED
                : complaint.Status;

            await _complaintRepository.SaveAsync(complaint);
            _unitOfWork.Commit();
            return managerComment.ToResult();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}