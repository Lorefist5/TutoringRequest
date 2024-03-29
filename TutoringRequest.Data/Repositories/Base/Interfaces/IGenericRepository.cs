﻿using System.Linq.Expressions;
using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Data.Repositories.Base.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    public T? FirstOrDefault(Expression<Func<T, bool>> predicate);
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    public IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
    public Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    public void Add(T entity);
    public Task AddAsync(T entity);
    public void Remove(T entity);
    public Task RemoveAsync(T entity);
    public Task<IEnumerable<T>> GetAllAsync();
    public IEnumerable<T> GetAll();
    public T? Update(Guid id, T newValues);
    public Task<T?> UpdateAsync(Guid id, T newValues);
}