using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IRoleRepository : IGenericRepository<Role>
{
    public Role GetTutorRole();
    public Role GetAdminRole();
    public Role GetStudentRole();
}
