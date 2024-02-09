namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    public ITutorRepository TutorRepository { get; }
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public IStudentRepository StudentRepository { get; }
    public IMajorRepository MajorRepository { get; }
    public ICourseRepository CourseRepository { get; }
    public IAdminAccountInfoRepository AdminAccountInfoRepository { get; }
    public IAdministratorRepository AdministratorRepository { get; }
    public IAdminRoleRepository AdminRoleRepository { get; }
    public void SaveChanges();
    public Task SaveChangesAsync();
}
