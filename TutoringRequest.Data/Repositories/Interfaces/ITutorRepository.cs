using System.Linq.Expressions;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Interfaces;

public interface ITutorRepository : IGenericRepository<Tutor>
{
    public List<Course> GetTutorCourses(Tutor tutor);
    public List<TutoringSection> GetTutoringSections(Tutor tutor);
}
