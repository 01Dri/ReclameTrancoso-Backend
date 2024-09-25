using Domain.Models;
using Domain.Models.DTOs.Comments;
using Domain.Models.DTOs.Complaint;

namespace Application.Mappers;

public static class ComplaintMapper
{
    public static Complaint ToEntity(this ComplaintCreateRequestDTO dto)
    {
        return new Complaint()
        {
            Title = dto.Title,
            Description = dto.Description,
            ComplaintType = dto.ComplaintType,
            AdditionalInformation1 = dto.AdditionalInformation1,
            AdditionalInformation2 = dto.AdditionalInformation2,
            AdditionalInformation3 = dto.AdditionalInformation3,
            IsAnonymous = dto.IsAnonymous,
            Status = dto.Status
        };
    }
    
    public static Complaint ToEntity(this ComplaintUpdateRequestDTO dto)
    {
        return new Complaint()
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            ComplaintType = dto.ComplaintType,
            AdditionalInformation1 = dto.AdditionalInformation1,
            AdditionalInformation2 = dto.AdditionalInformation2,
            AdditionalInformation3 = dto.AdditionalInformation3,
            IsAnonymous = dto.IsAnonymous,
            Status = dto.Status

        };
    }

    public static ComplaintDto ToResult(this Complaint complaint)
    {
        return new ComplaintDto()
        {
            Id = complaint.Id,
            Title = complaint.Title,
            Description = complaint.Description,
            ComplaintType = complaint.ComplaintType,
            AdditionalInformation1 = complaint.AdditionalInformation1,
            AdditionalInformation2 = complaint.AdditionalInformation2,
            AdditionalInformation3 = complaint.AdditionalInformation3,
            IsAnonymous = complaint.IsAnonymous,
            Status = complaint.Status,
            ManagerComment = new CommentDTO(complaint.Comment?.CommentId, complaint.Comment?.Comment?.Text)
        };
    } 
}