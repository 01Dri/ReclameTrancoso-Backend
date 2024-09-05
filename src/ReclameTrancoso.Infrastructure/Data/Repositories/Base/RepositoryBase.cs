using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
where TEntity : BaseEntity
{
    protected DataContext DataContext { get; set; }
    public DbSet<TEntity> DbSet { get; }
    public RepositoryBase(DataContext dataContext)
    {
        this.DataContext = dataContext;
        this.DbSet = this.DataContext.Set<TEntity>();
    }

    public async ValueTask<TEntity> SaveAsync(TEntity entity)
    {
        if (entity.Id == null)
        {
            this.DbSet.Entry(entity).State = EntityState.Added;
            await this.DbSet.AddAsync(entity);
        }
        else
        {
            this.DbSet.Entry(entity).State = EntityState.Modified;
            this.DbSet.Update(entity);
        }
        await this.DataContext.SaveChangesAsync();
        return entity;
    }

    public async ValueTask<TEntity> GetByIdAsync(long? id)
        => await this.DbSet.AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async ValueTask<IEnumerable<TEntity>> GetAsync()
        => await this.DbSet.AsNoTrackingWithIdentityResolution().ToListAsync();

    public async Task DeleteAsync(long? id)
    {
        var entity = await this.GetByIdAsync(id);
        this.DbSet.Remove(entity);
        await this.DataContext.SaveChangesAsync();
    }

    public async ValueTask<bool> ExistsByIdAsync(long? id)
        => await this.DbSet.AnyAsync(x => x.Id == id);

}