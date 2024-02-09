using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TutoringDbContext _tutoringDbContext;

    public UnitOfWork(TutoringDbContext tutoringDbContext, 
        ITutorRepository tutorRepository, 
        IAvailabilitySlotRepository availabilitySlotRepository, 
        IStudentRepository studentRepository,
        IMajorRepository majorRepository,
        ICourseRepository courseRepository)
    {
        _tutoringDbContext = tutoringDbContext;
        TutorRepository = tutorRepository;
        AvailabilitySlotRepository = availabilitySlotRepository;
        StudentRepository = studentRepository;
        MajorRepository = majorRepository;
        CourseRepository = courseRepository;
    }
    public ITutorRepository TutorRepository { get; }
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public IStudentRepository StudentRepository { get; }
    public IMajorRepository MajorRepository { get; }
    public ICourseRepository CourseRepository { get; }
    public void SaveChanges()
    {
        _tutoringDbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _tutoringDbContext.SaveChangesAsync();
    }
}
