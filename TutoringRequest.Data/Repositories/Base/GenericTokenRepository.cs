using Microsoft.EntityFrameworkCore;
using TutoringRequest.Data.Repositories.Base.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Data.Repositories.Base;

abstract public class GenericTokenRepository<T> : IGenericTokenRepository<T> where T : TokenBase
{
    private readonly TutoringDbContext _context;
    private readonly DbSet<T> _tokens;
    public GenericTokenRepository(TutoringDbContext context)
    {
        this._context = context;
        _tokens = _context.Set<T>();
    }
    public async Task CreateTokenAsync(T token)
    {
        await _tokens.AddAsync(token);
    }

    public async Task DeleteTokenAsync(Guid id)
    {
        T? token = await _tokens.FirstOrDefaultAsync(t => t.Id == id);

        if (token != null)
        {
            _tokens.Remove(token);
        }
    }
    public async Task<T?> GetActiveTokenForUserAsync(Guid userId)
    {
        return await _tokens
            .Where(t => t.AccountId == userId && t.IsActive && t.ExpirationTime > DateTime.UtcNow)
            .FirstOrDefaultAsync();
    }
    public async Task<T?> GetTokenByUniqueIdentifierAsync(Guid uniqueIdentifier)
    {
        return await _tokens.FirstOrDefaultAsync(t => t.Id.Equals(uniqueIdentifier));
    }


}
