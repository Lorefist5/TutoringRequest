namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public IMajorRepository MajorRepository { get; }
    public ICourseRepository CourseRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IResetTokenRepository ResetTokenRepository { get; }
    public void SaveChanges();
    public Task SaveChangesAsync();
}
