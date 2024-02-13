using TutoringRequest.Models.Domain;

namespace TutoringRequest.Api.Services.Interfaces
{
    public interface IResetTokenService
    {
        Task<bool> CanCreateResetTokenForUserAsync(Guid userId);
        Task<ResetToken> CreateResetTokenAsync(Guid userId);
        Task<ResetToken?> GetResetTokenByUniqueIdentifierAsync(Guid uniqueIdentifier);
        Task RemoveResetTokenAsync(Guid tokenId);
    }
}