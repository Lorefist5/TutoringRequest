using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class UnitOfWorkTest : IUnitOfWork
{

    public UnitOfWorkTest(
        IAvailabilitySlotRepository availabilitySlotRepository, 
        IMajorRepository majorRepository)
    {
        AvailabilitySlotRepository = availabilitySlotRepository;
        MajorRepository = majorRepository;
    }


    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }

    public IMajorRepository MajorRepository { get; }

    public ICourseRepository CourseRepository => throw new NotImplementedException();



    public IAccountRepository AccountRepository => throw new NotImplementedException();

    public IRoleRepository RoleRepository => throw new NotImplementedException();

    public IResetTokenRepository ResetTokenRepository => throw new NotImplementedException();

    public void SaveChanges()
    {
        Console.WriteLine();
    }

    public async Task SaveChangesAsync()
    {
        await Task.Run(() => Console.WriteLine(""));
    }
}
