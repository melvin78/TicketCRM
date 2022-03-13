using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using Agent = TicketCRM.DomainLayer.MainBoundedContext.SupportEntities.Agent;
using AgentDTO = IdentityServerAspNetIdentity.Configuration.AgentDTO;
using AgentFactory = TicketCRM.DomainLayer.MainBoundedContext.Factories.AgentFactory;

namespace TicketCRM.SupportModule
{
    public class AgentService : IAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPusherService _pusherService;

        public AgentService(IUnitOfWork unitOfWork,IMapper  mapper,IPusherService pusherService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pusherService = pusherService;
        }

        public bool AssignAgentWithToken(Guid userIdWithToken,Guid userIdToBeGivenToken)
        {
            var persistedAgentWithToken = _unitOfWork.Repository<Agent>()
                .FindAll(new UserAgentSpecificationByIdentitySpecification(userIdWithToken)).First();
            
            var persistedAgentToBeGivenToken = _unitOfWork.Repository<Agent>()
                .FindAll(new UserAgentSpecificationByIdentitySpecification(userIdToBeGivenToken)).First();

            if (persistedAgentWithToken != null)
            {

                var currentAgentWithToken = AgentFactory.CreateNewAgent(
                    persistedAgentWithToken.Username,
                    persistedAgentWithToken.DepartmentId,
                    persistedAgentWithToken.FirstName,
                    persistedAgentWithToken.SecondName,
                    persistedAgentWithToken.UserId,
                    persistedAgentWithToken.TokenAssignmentDate,
                    persistedAgentWithToken.HasToken=false
                );
                _unitOfWork.Repository<Agent>().Merge(persistedAgentWithToken, currentAgentWithToken);

            }
            
            if (persistedAgentToBeGivenToken != null)
            {

                var currentAgentToBeGivenToken = AgentFactory.CreateNewAgent(
                    persistedAgentToBeGivenToken.Username,
                    persistedAgentToBeGivenToken.DepartmentId,
                    persistedAgentToBeGivenToken.FirstName,
                    persistedAgentToBeGivenToken.SecondName,
                    persistedAgentToBeGivenToken.UserId,
                    persistedAgentToBeGivenToken.TokenAssignmentDate=DateTime.Now,
                    persistedAgentToBeGivenToken.HasToken=true
                );
                _unitOfWork.Repository<Agent>().Merge(persistedAgentToBeGivenToken, currentAgentToBeGivenToken);

            }

            return true;
        }

        public async Task<AgentDTO> GetAgentDetails(Guid agentId)
        {
            var agent = 
                _unitOfWork.Repository<Agent>().
                    FindAll(new UserAgentSpecificationByIdentitySpecification(agentId)).First();

            return _mapper.Map<AgentDTO>(agent);
        }

        public async Task<List<AgentDetailsDTO>> GetListOfAgents()
        {
            var agents = await _unitOfWork.Repository<Agent>().GetAllAsync();

            return _mapper.Map<List<AgentDetailsDTO>>(agents);
        }
    }
}