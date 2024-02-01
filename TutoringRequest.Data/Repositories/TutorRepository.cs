using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories;

public class TutorRepository : GenericRepository<Tutor>, ITutorRepository
{
    public TutorRepository(TutoringDbContext context) : base(context)
    {
        _entities.Include(e => e.AvailabilitySlots);

    }
    public List<Course> GetTutorCourses(Tutor tutor)
    {
        return tutor.Courses;
    }
    public List<TutoringSection> GetTutoringSections(Tutor tutor)
    {
        return tutor.TutoringSections;
    }

    public override Tutor? FirstOrDefault(Expression<Func<Tutor, bool>> predicate)
    {
        return _entities.Include(t => t.AvailabilitySlots).FirstOrDefault(predicate);
    }

}
