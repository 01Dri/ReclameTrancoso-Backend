using Domain.Interfaces;

namespace Domain.Models.DTOs;

public record GetByIdRequest(long Id) : IUseCaseRequest ;