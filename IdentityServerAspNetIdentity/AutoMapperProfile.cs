using IdentityServerAspNetIdentity.Configuration;

namespace IdentityServerAspNetIdentity
{
    public class AutoMapperProfile:AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Agent, AgentDTO>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<Organization, OrganizationDTO>();
        }
    }
}