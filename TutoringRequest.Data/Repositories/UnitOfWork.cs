using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TutoringDbContext _tutoringDbContext;
    
    public UnitOfWork(TutoringDbContext tutoringDbContext,ITutorRepository tutorRepository, IAvailabilitySlotRepository availabilitySlotRepository)
    {
        this._tutoringDbContext = tutoringDbContext;
        TutorRepository = tutorRepository;
        AvailabilitySlotRepository = availabilitySlotRepository;
    }
    public ITutorRepository TutorRepository { get; }
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public void SaveChanges()
    {
        _tutoringDbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _tutoringDbContext.SaveChangesAsync();
    }
}
