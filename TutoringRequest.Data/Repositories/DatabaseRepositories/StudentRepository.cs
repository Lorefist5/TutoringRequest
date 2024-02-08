using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(TutoringDbContext context) : base(context)
    {
    }
}
