using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class AdminAccountInfoRepository : GenericRepository<AdminAccountInfo>, IAdminAccountInfoRepository
{
    public AdminAccountInfoRepository(TutoringDbContext context) : base(context)
    {
    }
}
