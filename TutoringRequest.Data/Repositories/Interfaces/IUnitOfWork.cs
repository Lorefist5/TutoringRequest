namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    public ITutorRepository TutorRepository { get; }
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public IStudentRepository StudentRepository { get; }
    public IMajorRepository MajorRepository { get; }
    public void SaveChanges();
    public Task SaveChangesAsync();
}
