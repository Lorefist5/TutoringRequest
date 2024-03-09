using System.Linq.Expressions;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Base.Interfaces;

public interface IGenericRoleRepository
{
    Account? FirstOrDefault(Expression<Func<Account, bool>> predicate);
    Task<Account?> FirstOrDefaultAsync(Expression<Func<Account, bool>> predicate);
    public Task<IEnumerable<Account>> GetAllAsync();
    IEnumerable<Account> GetAll();
}