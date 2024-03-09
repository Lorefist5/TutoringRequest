using Microsoft.EntityFrameworkCore;
using TutoringRequest.Data.Repositories.Base;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class MajorRepository : GenericRepository<Major>, IMajorRepository
{
    public MajorRepository(TutoringDbContext context) : base(context)
    {
    }

    public List<Course> GetCoursesInMajor(Major major)
    {
        Major? m = _entities.Include(e => e.Courses).FirstOrDefault(m => m.Id == major.Id);
        if (m == null) return new List<Course>();
        return m.Courses;
    }
    public async Task<List<Course>> GetCoursesInMajorAsync(Major major)
    {
        Major? m = await _entities.Include(e => e.Courses).FirstOrDefaultAsync(m => m.Id == major.Id);
        if (m == null) return new List<Course>();
        return m.Courses;
    }
}
