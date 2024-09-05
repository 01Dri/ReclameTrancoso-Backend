namespace Domain.Interfaces;

public interface IUseCaseHandlerFactory
{
    IUseCaseHandler<TRequest, TResponse> CreateHandler<TRequest, TResponse>()
        where TRequest : IUseCaseRequest
        where TResponse : IUseCaseResponse;
}