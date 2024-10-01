using Domain.Interfaces;
using ReclameTrancoso.Domain.Enums;

namespace Domain.Models.DTOs.Complaint;

public class ComplaintCreateRequestDTO : IUseCaseRequest
{
    public long ResidentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ComplaintType ComplaintType { get; set; }
    public string? AdditionalInformation1 { get; set; }
    public string? AdditionalInformation2 { get; set; }
    public string? AdditionalInformation3 { get; set; }
    public bool IsAnonymous { get; set; }
    public ComplaintStatus Status { get; set; }
}