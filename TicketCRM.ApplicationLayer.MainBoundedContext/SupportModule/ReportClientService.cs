using AutoMapper;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class ReportClientService:IReportClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportClientService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<TicketReport>> GetTicketReports()
        {

            var clientTicketReport = await _unitOfWork.Repository<TicketReport>()
                .GetKeylessEntitiy("SELECT * FROM `centrino.email`.customer_tickets_reports");

            return  clientTicketReport;

        }

        public TicketReport GetTicketReportsByTicketNo(string ticketNo)
        {
            
            var clientTicketReport =  _unitOfWork.Repository<TicketReport>()
                .GetKeylessEntityByParams("SELECT * FROM `centrino.email`.customer_tickets_reports where TicketNo={0}",ticketNo);

            return  clientTicketReport;        
        }

        public List<TicketReport> GetAllTicketReportsByUserId(Guid userId)
        {
            var clientTicketReport =  _unitOfWork.Repository<TicketReport>()
                .GetAllKeylessEntityByParams("SELECT * FROM `centrino.email`.customer_tickets_reports where UserId={0}",userId.ToString());

            return  clientTicketReport;
            
        }
    }
}