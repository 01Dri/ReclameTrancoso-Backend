namespace ReclameTrancoso.Domain.Interfaces.Transactions;

public interface IUnitOfWork
{
    void Begin();
    void Commit();
    void Rollback();
}