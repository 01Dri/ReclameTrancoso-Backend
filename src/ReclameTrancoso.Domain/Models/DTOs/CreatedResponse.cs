using Domain.Interfaces;

namespace Domain.Models.DTOs;

public record CreatedResponse(long? id) : IUseCaseResponse;