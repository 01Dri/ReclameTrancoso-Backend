using ReclameTrancoso.Domain.Enums;
using ReclameTrancoso.Domain.Models;

namespace Domain.Models;

public class Complaint : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public ComplaintType ComplaintType { get; set; }
    public string? AdditionalInformation1 { get; set; }
    public string? AdditionalInformation2 { get; set; }
    public string? AdditionalInformation3 { get; set; }
    public bool IsAnonymous { get; set; }
    public ComplaintStatus Status { get; set; }
    public long? ResidentId { get; set; }
    public virtual Resident Resident { get; set; }
    public virtual ManagerComplaintComments? Comment { get; set; }

}