using Microsoft.EntityFrameworkCore;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories;

public class AvailabilitySlotRepository : GenericRepository<AvailabilitySlot>, IAvailabilitySlotRepository
{
    public AvailabilitySlotRepository(TutoringDbContext context) : base(context)
    {
    }

    public async Task<List<AvailabilitySlot>> GetTutorAvailabilitySlots(Tutor tutor)
    {
        return await _entities.Where(a => a.TutorId == tutor.Id).ToListAsync();
    }
}
