using Domain.Interfaces;

namespace Domain.Models;

public class GetRequestPaginated : IUseCaseRequest
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}