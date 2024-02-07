using TutoringRequest.Data.Repositories.Interfaces;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class UnitOfWorkTest : IUnitOfWork
{

    public UnitOfWorkTest(ITutorRepository tutorRepository, IAvailabilitySlotRepository availabilitySlotRepository, IStudentRepository studentRepository)
    {
        TutorRepository = tutorRepository;
        AvailabilitySlotRepository = availabilitySlotRepository;
        StudentRepository = studentRepository;
    }

    public ITutorRepository TutorRepository { get; }

    public IAvailabilitySlotRepository AvailabilitySlotRepository { get; }

    public IStudentRepository StudentRepository { get; }

    public void SaveChanges()
    {
        Console.WriteLine();
    }

    public async Task SaveChangesAsync()
    {
        await Task.Run(() => Console.WriteLine(""));
    }
}
