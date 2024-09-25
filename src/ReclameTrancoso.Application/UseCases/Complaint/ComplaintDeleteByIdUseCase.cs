using Domain.Interfaces;
using Domain.Models.DTOs;
using ReclameTrancoso.Domain.Interfaces.Transactions;
using ReclameTrancoso.Exceptions.Exceptions;

namespace Application.UseCases.Complaint;

public class ComplaintDeleteByIdUseCase : IUseCaseHandlerRes<DeleteRequest>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ComplaintDeleteByIdUseCase(IComplaintRepository complaintRepository, IUnitOfWork unitOfWork)
    {
        _complaintRepository = complaintRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteRequest request, CancellationToken cancellationToken)
    {

        _unitOfWork.Begin();
        try
        {

            if (!await _complaintRepository.ExistsByIdAsync(request.Id))
            {
                throw new NotFoundException("Ticket não existe.");
            }

            await _complaintRepository.DeleteAsync(request.Id);
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}