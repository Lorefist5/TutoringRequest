using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class UnitOfWorkTest : IUnitOfWork
{

    public UnitOfWorkTest(ITutorRepository tutorRepository, IAvailabilitySlotRepository availabilitySlotRepository, IStudentRepository studentRepository, IMajorRepository majorRepository)
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

    public void SaveChanges()
    {
        Console.WriteLine();
    }

    public async Task SaveChangesAsync()
    {
        await Task.Run(() => Console.WriteLine(""));
    }
}
