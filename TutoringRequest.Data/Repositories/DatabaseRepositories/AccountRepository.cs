using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Base;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.Enums;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(TutoringDbContext context, ITutorRepository tutorRepository) : base(context)
    {
        TutorRepository = tutorRepository;
    }
    public ITutorRepository TutorRepository { get; }

    
    public Account? GetAccountWithRoles(Expression<Func<Account, bool>> predicate)
    {
        return _entities.Include(a => a.Roles).FirstOrDefault(predicate);
    }

    public async Task<Account?> GetAccountWithRolesAsync(Expression<Func<Account, bool>> predicate)
    {

        return await _entities.Include(a => a.Roles).FirstOrDefaultAsync(predicate);
    }

}