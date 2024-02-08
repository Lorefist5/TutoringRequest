using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Api.Controllers;
using TutoringRequest.Api.Mapping;
using TutoringRequest.Data.Repositories.InMemoryRepositories;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Data.Repositories.TestRepositories;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;


namespace TutoringRequest.Test.Controller;

public class TutorAvailabilityTest
{
    IStudentRepository studentRepository;
    ITutorRepository tutorRepository;
    IAvailabilitySlotRepository availabilitySlotRepository;
    IMajorRepository majorRepository;
    IUnitOfWork unitOfWork;
    IMapper mapper;
    public TutorAvailabilityTest()
    {
        //Seed data
        var students = new List<Student>
    {
        new Student { Id = Guid.NewGuid(), StudentNumber = "S001", Name = "Alice" },
        new Student { Id = Guid.NewGuid(), StudentNumber = "S002", Name = "Bob" },
        // Add more students as needed
    };
        var tutors = new List<Tutor>
    {
        new Tutor { Id = Guid.NewGuid(), StudentNumber = "T001", TutorName = "Tutor1" },
        new Tutor { Id = Guid.NewGuid(), StudentNumber = "T002", TutorName = "Tutor2" },
        // Add more tutors as needed
    };
        var slots = new List<AvailabilitySlot>
    {
        new AvailabilitySlot { Id = Guid.NewGuid(), TutorId = tutors[0].Id, Day = DayOfWeek.Monday, StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(11) },
        new AvailabilitySlot { Id = Guid.NewGuid(), TutorId = tutors[0].Id, Day = DayOfWeek.Wednesday, StartTime = TimeSpan.FromHours(13), EndTime = TimeSpan.FromHours(15) },
        new AvailabilitySlot { Id = Guid.NewGuid(), TutorId = tutors[1].Id, Day = DayOfWeek.Tuesday, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(12) },
        // Add more slots as needed
    };
        foreach (var tutor in tutors)
            {
                tutor.AvailabilitySlots = slots.Where(slot => slot.TutorId == tutor.Id).ToList();
            }
        foreach (var student in students)
            {
                student.TutoringSections = new List<TutoringSection>
        {
            new TutoringSection { Id = Guid.NewGuid(), TutorId = tutors[0].Id, StudentId = student.Id },
            new TutoringSection { Id = Guid.NewGuid(), TutorId = tutors[1].Id, StudentId = student.Id },
            // Add more TutoringSections as needed
        };
        }

        //Services
        studentRepository = new StudentInMemoryRepo(students);
        tutorRepository = new TutorInMemoryRepo(tutors);
        availabilitySlotRepository = new AvailabilitySlotInMemoryRepo(slots);
        majorRepository = new MajorInMemoryRepo();
        unitOfWork = new UnitOfWorkTest(tutorRepository, availabilitySlotRepository, studentRepository,majorRepository);
        mapper = AutoMappingProfiles.Configure();
    }
    [Test]
    public async Task CheckOverLapTest()
    {
        TutorAvailabilitySlotController tutorAvailabilitySlotController = new TutorAvailabilitySlotController(unitOfWork, mapper);
        var response = await tutorAvailabilitySlotController.CreateTutorSlotByStudentNumber("T001", new AddAvailabilitySlotRequest() { Day = DayOfWeek.Sunday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(14, 0, 0) });

        Assert.IsInstanceOf<ConflictObjectResult>(response);

        // Extract the content from the response
        var conflictResult = response as ConflictObjectResult;
        Assert.IsNotNull(conflictResult);
        if (conflictResult.Value != null)
        {
            Assert.That("The new slot overlaps with an existing slot." == conflictResult.Value.ToString());
        }
    } 
}



// Start time = 11
// End time = 12
//
// Start time = 10 
// End time = 12
//