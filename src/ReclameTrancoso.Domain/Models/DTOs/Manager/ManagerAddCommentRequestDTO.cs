using Domain.Interfaces;

namespace Domain.Models.DTOs.Manager;

public class ManagerAddCommentRequestDTO : IUseCaseRequest
{
    public long? ManagerId { get; set; }
    public long? ComplaintId { get; set; }
    public string? Comment { get; set; }
}