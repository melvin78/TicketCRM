using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class TicketStatusService:ITicketStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddNewTicketStatus(TicketStatusDTO ticketStatusDto)
        {
            if ( ticketStatusDto == null) return 0;

            var ticketStatus = TicketStatusFactory.AddNewTicketStatus(ticketStatusDto.TicketStatusVal);
            
            return await _unitOfWork.Repository<TicketStatus>().AddAsync(ticketStatus);
        }

        public async Task<List<TicketStatus>> GetTicketStatus()
        {
            return await _unitOfWork.Repository<TicketStatus>().GetAllAsync();
        }
    }
}