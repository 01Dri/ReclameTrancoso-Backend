using Domain.Models;
using Domain.Models.DTOs.Manager;
using ReclameTrancoso.Domain.Models;

namespace Application.Mappers;

public static class ManagerComplaintCommentMapper
{

    public static ManagerComplaintComments ToEntity(this ManagerAddCommentRequestDTO dto)
    {
        return new ManagerComplaintComments()
        {
            ComplaintId = dto.ComplaintId,
            ManagerId = dto.ManagerId
        };
    }

    public static Comment ToCommentEntity(this ManagerAddCommentRequestDTO dto)
    {
        return new Comment()
        {
            Id = dto.Comment?.Id,
            Text = dto.Comment?.Text
        };

    }

    public static ManagerAddCommentResponseDTO ToResult(this ManagerComplaintComments entity)
    {
        return new ManagerAddCommentResponseDTO(entity.Id, entity.ComplaintId, entity.Comment?.Text);
    }
    
}