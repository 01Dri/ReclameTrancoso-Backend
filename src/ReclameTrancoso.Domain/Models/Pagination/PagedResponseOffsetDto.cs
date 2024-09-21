using Domain.Interfaces;

namespace Domain.Models.Pagination;


public record PagedResponseOffsetDto<T> : IUseCaseResponse
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    
    public List<T> Data { get; init; }
}