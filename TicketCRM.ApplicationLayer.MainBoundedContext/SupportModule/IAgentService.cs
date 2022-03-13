using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using AgentDTO = IdentityServerAspNetIdentity.Configuration.AgentDTO;

namespace TicketCRM.SupportModule
{
    public interface IAgentService
    {
        bool AssignAgentWithToken(Guid userIdWithToken,Guid userIdToBeGivenTocken);

        Task<AgentDTO> GetAgentDetails(Guid agentId);

        Task<List<AgentDetailsDTO>> GetListOfAgents();





    }
}