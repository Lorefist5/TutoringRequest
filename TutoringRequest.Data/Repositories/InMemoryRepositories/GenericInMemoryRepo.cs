using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Data.Repositories.TestRepositories;

abstract public class GenericInMemoryRepo<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly List<T> _entities;
    public GenericInMemoryRepo()
    {
        _entities = new List<T>();
    }
    public GenericInMemoryRepo(List<T> entities)
    {
        this._entities = entities;
    }

    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await Task.Run(() => _entities.Add(entity));
    }

    public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return _entities.AsQueryable().FirstOrDefault(predicate.Compile());
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.Run(() => _entities.AsQueryable().FirstOrDefault(predicate.Compile()));
    }

    public IEnumerable<T> GetAll()
    {
        return _entities;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.Run(() => _entities.AsEnumerable());
    }

    public void Remove(T entity)
    {
        _entities.Remove(entity);
    }

    public async Task RemoveAsync(T entity)
    {
        await Task.Run(() => _entities.Remove(entity));
    }

    public T? Update(Guid id, T newValues)
    {
        throw new NotImplementedException();
    }

    public Task<T?> UpdateAsync(Guid id, T newValues)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return _entities.AsQueryable().Where(predicate.Compile());
    }

    public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.Run(() => _entities.AsQueryable().Where(predicate.Compile()).ToList());
    }
}
