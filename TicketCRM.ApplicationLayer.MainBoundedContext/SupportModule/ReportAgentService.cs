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

        public List<TicketSummary> FindTicketSummaryBySaccoId(Guid ticketStatus, Guid SaccoId)
        {
            // SELECT * FROM `centrino.email`.ticket_summaries where TicketStatusId ='e70fc103-471c-ec11-b063-14cb19ba19a9' and SaccoId = '2fc42189-9497-4638-b5de-22ce8d46c595'
            var ticketSummaryReport =   _unitOfWork.Repository<TicketSummary>()
                .GetAllKeylessEntityByMultipleParamsByTicketStatusIdAndSaccoId(ticketStatus.ToString(),SaccoId.ToString());

            return  ticketSummaryReport;     
        }

        public TicketEmailReportSummary GetEmailTicketReportSummaries(string ticketNo)
        {
            var ticketSummaryReport = _unitOfWork.Repository<TicketEmailReportSummary>()
                .GetAllKeylessEntityByParams("SELECT * FROM `centrino.email`.support_ticket_summary where TicketNo={0}",
                    ticketNo).First();

         
            return  ticketSummaryReport;     
        }

        public async Task<List<TicketSummary>> FindResolvedTicketsBySaccoId(string saccoId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TicketSummary>> FindAllResolvedTickets()
        {
            throw new NotImplementedException();
        }
    }
}