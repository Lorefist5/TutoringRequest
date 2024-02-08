using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class StudentInMemoryRepo : GenericInMemoryRepo<Student>, IStudentRepository
{
    public StudentInMemoryRepo()
    {
    }

    public StudentInMemoryRepo(List<Student> entities) : base(entities)
    {
    }
}
