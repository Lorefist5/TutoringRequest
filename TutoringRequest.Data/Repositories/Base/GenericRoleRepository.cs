using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.Enums;

namespace TutoringRequest.Data.Repositories.Base;

abstract public class GenericRoleRepository : IGenericRoleRepository
{
    private readonly DbSet<Account> _entities;
    private readonly string _roleNameNeeded;
    public GenericRoleRepository(TutoringDbContext context, string role)
    {
        _entities = context.Set<Account>();
        _roleNameNeeded = role;
    }
    public Account? FirstOrDefault(Expression<Func<Account, bool>> predicate)
    {
        return QueryRole().FirstOrDefault(predicate);

    }
    public async Task<Account?> FirstOrDefaultAsync(Expression<Func<Account, bool>> predicate)
    {
        return await QueryRole().FirstOrDefaultAsync(predicate);
    }
    public IEnumerable<Account> GetAll()
    {
        return QueryRole().ToList();
    }
    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        var entities = await QueryRole().ToListAsync();
        return entities;
    }
    protected IQueryable<Account> QueryRole()
    {
        return _entities.Where(a => a.Roles.Any(r => r.RoleName == _roleNameNeeded));
    }
}
