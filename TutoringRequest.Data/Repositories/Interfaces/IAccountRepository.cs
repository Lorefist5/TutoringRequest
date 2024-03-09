using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{

    public Account? GetAccountWithRoles(Expression<Func<Account, bool>> predicate);
    public Task<Account?> GetAccountWithRolesAsync(Expression<Func<Account, bool>> predicate);
    public ITutorRepository TutorRepository { get; }
    
}
