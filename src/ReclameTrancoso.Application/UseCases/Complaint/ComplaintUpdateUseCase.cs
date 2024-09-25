using Application.Mappers;
using Domain.Interfaces;
using Domain.Models.DTOs.Complaint;
using FluentValidation;
using ReclameTrancoso.Domain.Interfaces.Transactions;

namespace Application.UseCases.Complaint;

public class ComplaintUpdateUseCase : IUseCaseHandler<ComplaintUpdateRequestDTO, ComplaintDto>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ComplaintUpdateRequestDTO> _validator;

    public ComplaintUpdateUseCase(IComplaintRepository complaintRepository, IUnitOfWork unitOfWork, IValidator<ComplaintUpdateRequestDTO> validator)
    {
        _complaintRepository = complaintRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }


    public async Task<ComplaintDto> Handle(ComplaintUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        _unitOfWork.Begin();
        try
        {
            var entity = request.ToEntity();
            await _complaintRepository.SaveAsync(entity);
            _unitOfWork.Commit();
            return entity.ToResult();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}