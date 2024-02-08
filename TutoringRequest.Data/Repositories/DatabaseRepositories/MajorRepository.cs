using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class MajorRepository : GenericRepository<Major>, IMajorRepository
{
    public MajorRepository(TutoringDbContext context) : base(context)
    {
    }
}
