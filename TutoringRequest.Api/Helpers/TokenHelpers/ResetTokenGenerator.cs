using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Api.Helpers.TokenHelpers;

public class ResetTokenGenerator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenGenerator _tokenGenerator;

    public ResetTokenGenerator(IUnitOfWork unitOfWork, TokenGenerator tokenGenerator)
    {
        this._unitOfWork = unitOfWork;
        this._tokenGenerator = tokenGenerator;
    }

    public async Task<bool> CanCreateResetTokenForUserAsync(Guid userId)
    {
        ResetToken? existingToken = await _unitOfWork.ResetTokenRepository.GetActiveTokenForUserAsync(userId);

        return existingToken == null;
    }

    public async Task<ResetToken> CreateResetTokenAsync(Guid userId)
    {
        Account? fetchedAccount = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(a => a.Id == userId);
        if (fetchedAccount == null)
        {
            throw new InvalidOperationException($"Account with Id {userId} not found.");
        }
        var existingToken = await _unitOfWork.ResetTokenRepository.GetActiveTokenForUserAsync(userId);

        if (existingToken != null)
        {
            existingToken.IsActive = false;
        }

        var newResetToken = new ResetToken
        {
            AccountId = userId,
            Token = _tokenGenerator.GenerateJwtTokenForReset(fetchedAccount),
            IsActive = true,
            ExpirationTime = DateTime.UtcNow.AddHours(1),
        };

        await _unitOfWork.ResetTokenRepository.CreateTokenAsync(newResetToken);
        await _unitOfWork.SaveChangesAsync();
        return newResetToken;
    }


    public async Task<ResetToken?> GetResetTokenByUniqueIdentifierAsync(Guid uniqueIdentifier)
    {
        return await _unitOfWork.ResetTokenRepository.GetTokenByUniqueIdentifierAsync(uniqueIdentifier);
    }

    public async Task RemoveResetTokenAsync(Guid tokenId)
    {
        await _unitOfWork.ResetTokenRepository.DeleteTokenAsync(tokenId);
    }
}
