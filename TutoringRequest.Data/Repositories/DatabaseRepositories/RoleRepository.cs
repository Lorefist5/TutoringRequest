using TutoringRequest.Data.Repositories.Base;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.Enums;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(TutoringDbContext context) : base(context)
    {
    }

    public Role GetAdminRole()
    {
        Role? adminRole = _entities.FirstOrDefault(r => r.RoleName == DefaultRoles.Admin.ToString());
        if (adminRole == null) throw new Exception("Threw exception while trying to the the admin role in the GetAdminRole method on the RoleRepository(Database)");
        return adminRole;
    }

    public Role GetStudentRole()
    {
        Role? studentRole = _entities.FirstOrDefault(r => r.RoleName == DefaultRoles.Student.ToString());
        if (studentRole == null) throw new Exception("Threw exception while trying to the the student role in the GetStudentRole method on the RoleRepository(Database)");
        return studentRole;
    }

    public Role GetTutorRole()
    {
        Role? tutorRole = _entities.FirstOrDefault(r => r.RoleName == DefaultRoles.Tutor.ToString());
        if (tutorRole == null) throw new Exception("Threw exception while trying to the the tutor role in the GetTutorRole method on the RoleRepository(Database)");
        return tutorRole;
    }
}
