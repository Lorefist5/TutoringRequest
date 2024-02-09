using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class UnitOfWorkTest : IUnitOfWork
{

    public UnitOfWorkTest(
        ITutorRepository tutorRepository, 
        IAvailabilitySlotRepository availabilitySlotRepository, 
        IStudentRepository studentRepository, 
        IMajorRepository majorRepository)
    {
        TutorRepository = tutorRepository;
        AvailabilitySlotRepository = availabilitySlotRepository;
        StudentRepository = studentRepository;
        MajorRepository = majorRepository;
    }

    public ITutorRepository TutorRepository { get; }

    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }

    public IStudentRepository StudentRepository { get; }
    public IMajorRepository MajorRepository { get; }

    public ICourseRepository CourseRepository => throw new NotImplementedException();

    public IAdminAccountInfoRepository AdminAccountInfoRepository => throw new NotImplementedException();

    public IAdministratorRepository AdministratorRepository => throw new NotImplementedException();

    public IAdminRoleRepository AdminRoleRepository => throw new NotImplementedException();

    public void SaveChanges()
    {
        Console.WriteLine();
    }

    public async Task SaveChangesAsync()
    {
        await Task.Run(() => Console.WriteLine(""));
    }
}
