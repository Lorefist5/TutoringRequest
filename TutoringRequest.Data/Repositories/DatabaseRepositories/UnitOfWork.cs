using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TutoringDbContext _tutoringDbContext;

    public UnitOfWork(TutoringDbContext tutoringDbContext, 
        IAvailabilitySlotRepository availabilitySlotRepository, 
        IMajorRepository majorRepository,
        ICourseRepository courseRepository,
        IAccountRepository accountRepository,
        IRoleRepository roleRepository
        )
    {
        _tutoringDbContext = tutoringDbContext;
        AvailabilitySlotRepository = availabilitySlotRepository;
        MajorRepository = majorRepository;
        CourseRepository = courseRepository;
        AccountRepository = accountRepository;
        RoleRepository = roleRepository;
    }
    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }
    public IMajorRepository MajorRepository { get; }
    public ICourseRepository CourseRepository { get; }


    public IAccountRepository AccountRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public void SaveChanges()
    {
        _tutoringDbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _tutoringDbContext.SaveChangesAsync();
    }
}
