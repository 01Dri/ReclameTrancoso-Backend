namespace Domain.Interfaces;

public interface IUseCaseHandlerRes <TRequest>
    where TRequest : IUseCaseRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);

}