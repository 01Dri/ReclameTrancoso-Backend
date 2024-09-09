using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using ReclameTrancoso.Domain.Interfaces.Transactions;

namespace Infrastructure.Data.UnitOfWork;

public class UnitOfWorkEF : IUnitOfWork
{
    private readonly DataContext _context;
    public IDbContextTransaction Transaction { get; set; }

    public UnitOfWorkEF(DataContext context)
    {
        _context = context;
    }

    public void Begin()
        => this.Transaction = _context.Database.BeginTransaction();

    public void Commit()
        => this.Transaction.Commit();

    public void Rollback()
        => this.Transaction.Rollback();


}