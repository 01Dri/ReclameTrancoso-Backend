using Domain.Interfaces;

namespace Domain.Models.DTOs.Manager;

public record ManagerAddCommentResponseDTO(long? Id, long? ComplaintId, string? Comment) : IUseCaseResponse;