using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories;

public class AvailabilitySlotRepository : GenericRepository<AvailabilitySlot>, IAvailabilitySlotRepository
{
    public AvailabilitySlotRepository(TutoringDbContext context) : base(context)
    {
    }
}
