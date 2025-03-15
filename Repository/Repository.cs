using HospitalManagement.DataAccess;
using HospitalManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Repository;

public abstract class Repository<TEntity>(HospitalContext context) : IRepository<TEntity> where TEntity : class
{
    //private DbSet<TEntity> Entity => context.Set<TEntity>();

    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public IQueryable<TEntity> GetAll()
    {
        return context.Set<TEntity>().AsQueryable();
    }

    public TEntity GetById(int id) =>
        context.Set<TEntity>().Find(id);

    public async Task<TEntity> GetByIdAsync(int id) =>
        await context.Set<TEntity>().FindAsync(id);

    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
}
