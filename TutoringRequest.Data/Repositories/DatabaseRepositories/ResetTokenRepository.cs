using TutoringRequest.Data.Repositories.Base;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class ResetTokenRepository : GenericTokenRepository<ResetToken>, IResetTokenRepository
{
    public ResetTokenRepository(TutoringDbContext context) : base(context)
    {
    }
}
