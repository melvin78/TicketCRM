using AutoMapper;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class TicketAssignmentService : ITicketAssignmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TicketAssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TicketAssignment>> GetAllAssignedTickets()
        {
            return await _unitOfWork.Repository<TicketAssignment>()
                .GetKeylessEntitiy("SELECT * FROM `centrino.email`.tickets_assigned_per_agent");
        }

        public TicketAssignment GetAssignedTicketReportByTicketNo(string ticketNo)
        {

            return _unitOfWork.Repository<TicketAssignment>()
                .GetAllKeylessEntityByParams(
                    "SELECT * FROM `centrino.email`.tickets_assigned_per_agent WHERE TicketNo={0}", ticketNo).First();

        }

        public async Task<List<TicketAssignment>> GetAssignedTicketReportByAgentId(Guid agentId)
        {
            
            return _unitOfWork.Repository<TicketAssignment>()
                .GetAllKeylessEntityByParams(
                    "SELECT * FROM `centrino.email`.tickets_assigned_per_agent WHERE CareTaker={0}",
                    agentId.ToString());
        }
    }
}