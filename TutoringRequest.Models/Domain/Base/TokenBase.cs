namespace TutoringRequest.Models.Domain.Base;

abstract public class TokenBase : BaseEntity
{
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public string Token { get; set; }
    public bool IsActive { get; set; }
    public DateTime ExpirationTime { get; set; }
}
