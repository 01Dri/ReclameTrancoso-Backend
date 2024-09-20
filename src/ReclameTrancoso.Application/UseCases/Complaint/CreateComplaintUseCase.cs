using Application.Mappers;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Complaint;
using FluentValidation;
using ReclameTrancoso.Domain.Enums;
using ReclameTrancoso.Domain.Interfaces.Transactions;

namespace Application.UseCases.Complaint;

public class CreateComplaintUseCase : IUseCaseHandler<ComplaintCreateRequestDTO, CreatedResponse>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IResidentComplaintRepository _residentComplaintRepository;
    private readonly IResidentRepository _residentRepository;
    private readonly IValidator<ComplaintCreateRequestDTO> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateComplaintUseCase(IComplaintRepository complaintRepository, IResidentComplaintRepository residentComplaintRepository, IResidentRepository residentRepository, IValidator<ComplaintCreateRequestDTO> validator, IUnitOfWork unitOfWork)
    {
        _complaintRepository = complaintRepository;
        _residentComplaintRepository = residentComplaintRepository;
        _residentRepository = residentRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }


    public async Task<CreatedResponse> Handle(ComplaintCreateRequestDTO? request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        _unitOfWork.Begin();

        try
        {

            var resident = await _residentRepository.GetResidentByIdAsync(request.ResidentId);
            var entity = request.ToEntity();
            entity.Status = ComplaintStatus.NO_TREATMENT;
            await _complaintRepository.SaveAsync(entity);
            var residentComplaint = new ResidentComplaint()
            {
                Complaint = entity,
                Resident = resident,
                ComplaintId = entity.Id,
                ResidentId = resident?.Id
            };
            await _residentComplaintRepository.SaveAsync(residentComplaint);
            _unitOfWork.Commit();
            return new CreatedResponse(residentComplaint.Id);
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}