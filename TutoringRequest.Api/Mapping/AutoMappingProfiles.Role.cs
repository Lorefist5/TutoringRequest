using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Roles;

namespace TutoringRequest.Api.Mapping;

public partial class AutoMappingProfiles
{
    public void ConfigureRoleMappings()
    {
        CreateMap<RoleDto, Role>().ReverseMap();
    }
}
