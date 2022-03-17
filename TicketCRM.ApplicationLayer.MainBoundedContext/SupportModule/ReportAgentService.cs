using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class ReportAgentService:IReportAgentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportAgentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TicketReport>> GetTicketReports()
        {
            var agentTicketReport = await _unitOfWork.Repository<TicketReport>()
                .GetKeylessEntitiy("SELECT * FROM `centrino.email`.customer_tickets_reports");

            return  agentTicketReport;
   
        }

        public List<TicketSummary> GetTicketSummaries(Guid ticketStatus)
        {
            var ticketSummaryReport =   _unitOfWork.Repository<TicketSummary>()
                .GetAllKeylessEntityByParams("SELECT * FROM `centrino.email`.ticket_summaries where TicketStatusId={0}",ticketStatus.ToString());

            return  ticketSummaryReport;     
        }

        public List<TicketSummary> FindTicketSummaryByOrganizationId(Guid ticketStatus, Guid organizationId)
        {
            var ticketSummaryReport =   _unitOfWork.Repository<TicketSummary>()
                .GetAllKeylessEntityByMultipleParamsByTicketStatusIdAndOrganizationId(ticketStatus.ToString(),organizationId.ToString());

            return  ticketSummaryReport;     
        }

        public TicketEmailReportSummary GetEmailTicketReportSummaries(string ticketNo)
        {
            var ticketSummaryReport = _unitOfWork.Repository<TicketEmailReportSummary>()
                .GetAllKeylessEntityByParams("SELECT * FROM `centrino.email`.support_ticket_summary where TicketNo={0}",
                    ticketNo).First();

         
            return  ticketSummaryReport;     
        }

        public async Task<List<TicketSummary>> FindResolvedTicketsByOrganizationId(string organizationId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TicketSummary>> FindAllResolvedTickets()
        {
            throw new NotImplementedException();
        }
    }
}