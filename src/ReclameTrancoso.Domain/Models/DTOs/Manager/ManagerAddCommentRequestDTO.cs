using Domain.Interfaces;
using Domain.Models.DTOs.Comments;

namespace Domain.Models.DTOs.Manager;

public class ManagerAddCommentRequestDTO : IUseCaseRequest
{
    public long? ManagerId { get; set; }
    public long? ComplaintId { get; set; }
    
    public CommentDTO Comment { get; set; }
}