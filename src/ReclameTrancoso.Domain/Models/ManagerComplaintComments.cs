using Domain.Models;
using Domain.Models.DTOs.Union;

namespace ReclameTrancoso.Domain.Models;

public class UnionComplaintComments : BaseEntity
{
    public long UnionId { get; set; }
    public virtual Manager Manager { get; set; }

    public long ComplaintId { get; set; }
    public virtual Complaint Complaint { get; set; }

    public long CommentId { get; set; }
    public virtual Comment Comment { get; set; }
    
    
}