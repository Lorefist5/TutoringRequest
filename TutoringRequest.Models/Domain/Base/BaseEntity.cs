namespace TutoringRequest.Models.Domain.Base;

abstract public class BaseEntity
{
    public Guid Id { get; set; }
    public Guid? CreatedById { get; set; }
    public Guid? ModifiedById { get; set; }
}
