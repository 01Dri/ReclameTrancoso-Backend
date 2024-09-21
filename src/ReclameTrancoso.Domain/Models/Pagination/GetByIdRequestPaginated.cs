using Domain.Interfaces;

namespace Domain.Models;

public class GetByIdRequestPaginated : IUseCaseRequest
{
    public long Id { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}