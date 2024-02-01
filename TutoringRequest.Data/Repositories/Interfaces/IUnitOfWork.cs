namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    public ITutorRepository TutorRepository { get; }
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public void SaveChanges();
    public Task SaveChangesAsync();
}
