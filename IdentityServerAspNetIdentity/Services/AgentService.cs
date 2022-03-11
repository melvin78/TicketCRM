using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Configuration;
using IdentityServerAspNetIdentity.Data;

namespace IdentityServerAspNetIdentity.Services
{
    public class AgentService:IAgentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AgentService(ApplicationDbContext  applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddNewAgent(AgentDTO agentDto)
        {

            var agent = AgentFactory.CreateNewAgent(
                agentDto.Username,
                agentDto.DepartmentId,
                agentDto.FirstName,
                agentDto.SecondName,
                agentDto.UserId,
                agentDto.TokenAssignmentDate=null
            );
            _applicationDbContext.Set<Agent>().Add(agent);

          return  await _applicationDbContext.SaveChangesAsync() ==1;
        }
    }
}