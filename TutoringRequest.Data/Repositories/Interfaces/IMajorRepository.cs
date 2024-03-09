using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IMajorRepository : IGenericRepository<Major>
{
    public List<Course> GetCoursesInMajor(Major major);
    public Task<List<Course>> GetCoursesInMajorAsync(Major major);
}