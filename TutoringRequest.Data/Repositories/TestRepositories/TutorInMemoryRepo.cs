using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class TutorInMemoryRepo : GenericInMemoryRepo<Tutor>, ITutorRepository
{
    public TutorInMemoryRepo()
    {
    }

    public TutorInMemoryRepo(List<Tutor> entities) : base(entities)
    {
    }

    public List<Course> GetTutorCourses(Tutor tutor)
    {
        return tutor.Courses.ToList();
    }

    public List<TutoringSection> GetTutoringSections(Tutor tutor)
    {
        return tutor.TutoringSections.ToList();
    }
}
