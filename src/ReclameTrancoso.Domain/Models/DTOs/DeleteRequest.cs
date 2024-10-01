using Domain.Interfaces;

namespace Domain.Models.DTOs;

public class DeleteRequest : IUseCaseRequest
{
    public long Id { get; set; }

    public DeleteRequest(long id)
    {
        Id = id;
    }
}