using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _entities;
    public GenericRepository(TutoringDbContext context)
    {
        this._context = context;
        _entities = context.Set<T>();
    }
    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return _entities.FirstOrDefault(predicate);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.FirstOrDefaultAsync(predicate);
    }

    public IEnumerable<T> GetAll()
    {
        return _entities.ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public void Remove(T entity)
    {
        var newEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);
        if(newEntity != null)
        {
            _entities.Remove(newEntity);
        }
    }

    public async Task RemoveAsync(T entity)
    {
        var newEntity = await _entities.FirstOrDefaultAsync(e => e.Id == entity.Id);
        if(newEntity != null)
        {
            _entities.Remove(newEntity);
        }
    }

    public IEnumerable<T> Where(Expression<Func<T, bool>> predicate) => _entities.Where(predicate);
    

    public Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate) => _entities.Where(predicate).ToListAsync();
    
}
