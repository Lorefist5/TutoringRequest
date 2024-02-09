using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class AdminRoleRepository : GenericRepository<AdminRole>, IAdminRoleRepository
{
    public AdminRoleRepository(TutoringDbContext context) : base(context)
    {
    }
}
