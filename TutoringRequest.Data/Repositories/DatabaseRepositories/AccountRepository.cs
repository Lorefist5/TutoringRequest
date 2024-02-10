using Microsoft.EntityFrameworkCore;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.Enums;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(TutoringDbContext context) : base(context)
    {
    }
    public async Task<List<Account>> GetTutorsAsync()
    {
        var tutorRoleName = DefaultRoles.Tutor.ToString();
        var tutors = await _entities.Include(a => a.Roles).Where(a => a.Roles.Any(r => r.RoleName == tutorRoleName)).ToListAsync();
        return tutors;
    }
    public List<Account> GetTutors()
    {
        var tutorRoleName = DefaultRoles.Tutor.ToString();
        var tutors = _entities.Include(a => a.Roles).Where(a => a.Roles.Any(r => r.RoleName == tutorRoleName)).ToList();
        return tutors;
    }
}