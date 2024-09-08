namespace Domain.Interfaces
{
    public interface IUseCaseHandler<TRequest, TResponse>
        where TRequest : IUseCaseRequest
        where TResponse : IUseCaseResponse
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}