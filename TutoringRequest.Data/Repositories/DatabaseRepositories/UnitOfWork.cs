using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TutoringDbContext _tutoringDbContext;

    public UnitOfWork(TutoringDbContext tutoringDbContext, 
        ITutorRepository tutorRepository, 
        IAvailabilitySlotRepository availabilitySlotRepository, 
        IStudentRepository studentRepository,
        IMajorRepository majorRepository)
    {
        _tutoringDbContext = tutoringDbContext;
        TutorRepository = tutorRepository;
        AvailabilitySlotRepository = availabilitySlotRepository;
        StudentRepository = studentRepository;
        MajorRepository = majorRepository;
    }
    public ITutorRepository TutorRepository { get; }
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public IStudentRepository StudentRepository { get; }
    public IMajorRepository MajorRepository { get; }
    public void SaveChanges()
    {
        _tutoringDbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _tutoringDbContext.SaveChangesAsync();
    }
}
