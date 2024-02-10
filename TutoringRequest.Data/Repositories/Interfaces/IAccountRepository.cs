using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{
    public Task<List<Account>> GetTutorsAsync();
    public List<Account> GetTutors();
}
