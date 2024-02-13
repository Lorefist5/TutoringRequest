using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Data.Repositories.DatabaseRepositories;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
}
