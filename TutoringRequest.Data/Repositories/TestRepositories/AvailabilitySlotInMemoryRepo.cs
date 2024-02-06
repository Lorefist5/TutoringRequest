using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.TestRepositories;

public class AvailabilitySlotInMemoryRepo : IAvailabilitySlotRepository
{
    public void Add(AvailabilitySlot entity)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(AvailabilitySlot entity)
    {
        throw new NotImplementedException();
    }

    public AvailabilitySlot? FirstOrDefault(Expression<Func<AvailabilitySlot, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<AvailabilitySlot?> FirstOrDefaultAsync(Expression<Func<AvailabilitySlot, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AvailabilitySlot> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AvailabilitySlot>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public List<AvailabilitySlot> GetTutorAvailabilitySlots(Tutor tutor)
    {
        throw new NotImplementedException();
    }

    public Task<List<AvailabilitySlot>> GetTutorAvailabilitySlotsAsync(Tutor tutor)
    {
        throw new NotImplementedException();
    }

    public void Remove(AvailabilitySlot entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(AvailabilitySlot entity)
    {
        throw new NotImplementedException();
    }

    public AvailabilitySlot? Update(Guid id, AvailabilitySlot newValues)
    {
        throw new NotImplementedException();
    }

    public Task<AvailabilitySlot?> UpdateAsync(Guid id, AvailabilitySlot newValues)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AvailabilitySlot> Where(Expression<Func<AvailabilitySlot, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<List<AvailabilitySlot>> WhereAsync(Expression<Func<AvailabilitySlot, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}
