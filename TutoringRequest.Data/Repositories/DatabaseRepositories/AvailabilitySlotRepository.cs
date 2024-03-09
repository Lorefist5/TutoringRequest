using Microsoft.EntityFrameworkCore;
using TutoringRequest.Data.Repositories.Base;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;

public class AvailabilitySlotRepository : GenericRepository<AvailabilitySlot>, IAvailabilitySlotRepository
{

    public AvailabilitySlotRepository(TutoringDbContext context) : base(context)
    {
    }

    //public List<AvailabilitySlot> GetTutorAvailabilitySlots(Tutor tutor)
    //{
    //    return _entities.Where(a => a.TutorId == tutor.Id).ToList();
    //}

    //public async Task<List<AvailabilitySlot>> GetTutorAvailabilitySlotsAsync(Tutor tutor)
    //{
    //    return await _entities.Where(a => a.TutorId == tutor.Id).ToListAsync();
    //}
}
