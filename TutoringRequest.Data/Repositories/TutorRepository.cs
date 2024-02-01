using Microsoft.EntityFrameworkCore;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories;

public class TutorRepository : GenericRepository<Tutor>, ITutorRepository
{
    public TutorRepository(TutoringDbContext context) : base(context)
    {
    }
    public List<Course> GetTutorCourses(Tutor tutor)
    {
        return tutor.Courses;
    }
    public List<TutoringSection> GetTutoringSections(Tutor tutor)
    {
        return tutor.TutoringSections;
    }
}
