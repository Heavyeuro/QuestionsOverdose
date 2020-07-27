using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.DAL.Interfaces;

public class RepositoryBase<T> : IRepositoryBase<T>
    where T : class
{
    public RepositoryBase(EFContext context)
    {
        RepositoryContext = context;
    }

    private EFContext RepositoryContext { get; }

    public virtual IQueryable<T> FindAll()
    {
        return RepositoryContext.Set<T>().AsNoTracking();
    }

    public virtual async Task<List<T>> FindAllAsync()
    {
        return await RepositoryContext.Set<T>().ToListAsync();
    }

    public virtual async Task<List<T>> GetPartlyAsync(int skip, int take)
    {
        return await RepositoryContext.Set<T>().Skip(skip).Take(take).ToListAsync();
    }

    public virtual async Task<int> CountAsync()
    {
        return await RepositoryContext.Set<T>().CountAsync();
    }

    public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return RepositoryContext.Set<T>()
            .Where(expression).AsNoTracking();
    }

    public virtual void Update(T entity)
    {
        RepositoryContext.Set<T>().Update(entity);
    }

    public virtual void Delete(T entity)
    {
        RepositoryContext.Set<T>().Remove(entity);
    }

    public async Task CreateAsync(T entity)
    {
        await RepositoryContext.AddAsync(entity);
    }

    public void Create(T entity)
    {
        RepositoryContext.Add(entity);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await RepositoryContext.FindAsync<T>(id);
    }
}