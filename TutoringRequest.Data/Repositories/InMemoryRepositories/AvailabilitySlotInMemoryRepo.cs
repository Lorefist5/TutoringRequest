using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class AvailabilitySlotInMemoryRepo : GenericInMemoryRepo<AvailabilitySlot>, IAvailabilitySlotRepository
{
    public AvailabilitySlotInMemoryRepo()
    {
        
    }
    public AvailabilitySlotInMemoryRepo(List<AvailabilitySlot> entities) : base(entities)
    {
    }

    //public async Task<List<AvailabilitySlot>> GetTutorAvailabilitySlotsAsync(Tutor tutor)
    //{
    //    return await Task.Run( () => _entities.Where(a => a.TutorId == tutor.Id).ToList());
    //}



    //public List<AvailabilitySlot> GetTutorAvailabilitySlots(Tutor tutor)
    //{
    //    return _entities.Where(a => a.TutorId == tutor.Id).ToList();
    //}
}
