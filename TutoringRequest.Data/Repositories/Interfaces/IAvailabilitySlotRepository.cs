using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IAvailabilitySlotRepository : IGenericRepository<AvailabilitySlot>
{
    //public Task<List<AvailabilitySlot>> GetTutorAvailabilitySlotsAsync(Tutor tutor);
    //public List<AvailabilitySlot> GetTutorAvailabilitySlots(Tutor tutor);
}