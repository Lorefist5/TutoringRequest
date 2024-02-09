using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(TutoringDbContext context) : base(context)
    {
    }
}
