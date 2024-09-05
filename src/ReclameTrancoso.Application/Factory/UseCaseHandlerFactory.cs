using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Factory;

public class UseCaseHandlerFactory : IUseCaseHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public UseCaseHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUseCaseHandler<TRequest, TResponse> CreateHandler<TRequest, TResponse>()
        where TRequest : IUseCaseRequest
        where TResponse : IUseCaseResponse
    {
        return _serviceProvider.GetRequiredService<IUseCaseHandler<TRequest, TResponse>>();
    }
}