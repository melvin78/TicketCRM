using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Configuration;

namespace IdentityServerAspNetIdentity.Services
{
    public interface IAgentService
    {
        Task<bool> AddNewAgent(AgentDTO agentDto);
    }
}