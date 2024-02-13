using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Data.Repositories.Base.Interfaces;

public interface IGenericTokenRepository<T> where T : TokenBase
{
    // Create a new token asynchronously
    public Task CreateTokenAsync(T token);

    // Get a token by its unique identifier asynchronously
    public Task<T?> GetTokenByUniqueIdentifierAsync(Guid uniqueIdentifier);

    // Delete a token asynchronously
    public Task DeleteTokenAsync(Guid id);
    public Task<T?> GetActiveTokenForUserAsync(Guid userId);
}
