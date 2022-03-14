using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketReportAgentController : ControllerBase
    {
        private readonly IReportAgentService _reportAgentService;
        private readonly IMapper _mapper;

        public TicketReportAgentController(IReportAgentService reportAgentService,IMapper mapper)
        {
            _reportAgentService = reportAgentService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("get-ticket-reports-agent")]
        public async Task<ActionResult<IEnumerable<TicketReportAgentDTO>>> GetTicketClientReport()
        {
            var ticketClientReports = await _reportAgentService.GetTicketReports();
            return Ok(_mapper.Map<List<TicketReportAgentDTO>>(ticketClientReports));
          
        }
        [HttpGet]
        [Route("get-new-tickets")]
        public ActionResult<IEnumerable<TicketSummary>> GetNewTickets()
        {
            var ticketClientReports =  _reportAgentService.GetTicketSummaries(Guid.Parse("0e06e836-471c-ec11-b063-14cb19ba19a9"));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-organization-new-tickets/{organizationId}")]
        public ActionResult<IEnumerable<TicketSummary>> GetOrganizationNewTickets(string organizationId)
        {
            var ticketClientReports =  _reportAgentService.FindTicketSummaryByOrganizationId(Guid.Parse("0e06e836-471c-ec11-b063-14cb19ba19a9"),Guid.Parse(organizationId));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
          
        [HttpGet]
        [Route("get-organization-closed-tickets/{saccoId}")]
        public ActionResult<IEnumerable<TicketSummary>> GetOrganizationClosedTickets(string organizationId)
        {
            var ticketClientReports =  _reportAgentService.FindTicketSummaryByOrganizationId(Guid.Parse("35ba51b2-9987-4d5e-b3a2-460584a23deb"),Guid.Parse(organizationId));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-organization-opened-ticket/{saccoId}")]
        public ActionResult<IEnumerable<TicketSummary>> GetOrganizationOpenedTickets(string organizationId)
        {
            var ticketClientReports =  _reportAgentService.FindTicketSummaryByOrganizationId(Guid.Parse("2f049c19-471c-ec11-b063-14cb19ba19a9"),Guid.Parse(organizationId));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-organization-re-opened-ticket/{organizationId}")]
        public ActionResult<IEnumerable<TicketSummary>> GetOrganizationReopenedTickets(string organizationId)
        {
            var ticketClientReports =  _reportAgentService.FindTicketSummaryByOrganizationId(Guid.Parse("3e763c25-5677-4ea4-a754-9b3d9b09c463"),Guid.Parse(organizationId));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-organization-overdue-tickets/{saccoId}")]
        public ActionResult<IEnumerable<TicketSummary>> GetOverdueOrganizationTickets(string organizationId)
        {
            var ticketClientReports =  _reportAgentService.FindTicketSummaryByOrganizationId(Guid.Parse("e70fc103-471c-ec11-b063-14cb19ba19a9"),Guid.Parse(organizationId));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        
        [HttpGet]
        [Route("get-organization-transferredTickets/{organizationId}")]
        public ActionResult<IEnumerable<TicketSummary>> GetOrganizationTransferredTickets(string organizationId)
        {
            var ticketClientReports =  _reportAgentService.FindTicketSummaryByOrganizationId(Guid.Parse("1ad2bda2-c923-ec11-8172-84a93e1f9479"),Guid.Parse(organizationId));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }   
        
        [HttpGet]
        [Route("get-organization-resolved-tickets/{organizationId}")]
        public ActionResult<IEnumerable<TicketSummary>> GetSaccoResolvedTickets(string organizationId)
        {
            var ticketClientReports =  _reportAgentService.FindTicketSummaryByOrganizationId(Guid.Parse("02d5e20e-471c-ec11-b063-14cb19ba19a9"),Guid.Parse(organizationId));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        
        
        
        [HttpGet]
        [Route("get-organization-opened-tickets")]
        public ActionResult<IEnumerable<TicketSummary>> GetOpenedTickets()
        {
            
            
            var ticketClientReports =  _reportAgentService.GetTicketSummaries(Guid.Parse("2f049c19-471c-ec11-b063-14cb19ba19a9"));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-closed-tickets")]
        public ActionResult<IEnumerable<TicketSummary>> GetClosedTickets()
        {
            var ticketClientReports =  _reportAgentService.GetTicketSummaries(Guid.Parse("35ba51b2-9987-4d5e-b3a2-460584a23deb"));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-reopened-ticket")]
        public ActionResult<IEnumerable<TicketSummary>> GetReOpenedTickets()
        {
            var ticketClientReports =  _reportAgentService.GetTicketSummaries(Guid.Parse("3e763c25-5677-4ea4-a754-9b3d9b09c463"));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-overdue-tickets")]
        public ActionResult<IEnumerable<TicketSummary>> GetOverDueTickets()
        {
            var ticketClientReports =  _reportAgentService.GetTicketSummaries(Guid.Parse("e70fc103-471c-ec11-b063-14cb19ba19a9"));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        [HttpGet]
        [Route("get-transferred-tickets")]
        public  ActionResult<IEnumerable<TicketSummary>> GetTransferredTickets()
        {
            var ticketClientReports =  _reportAgentService.GetTicketSummaries(Guid.Parse("1ad2bda2-c923-ec11-8172-84a93e1f9479"));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        [HttpGet]
        [Route("get-resolved-tickets")]
        public ActionResult<IEnumerable<TicketSummary>> GetResolvedTickets()
        {
            var ticketClientReports =  _reportAgentService.GetTicketSummaries(Guid.Parse("02d5e20e-471c-ec11-b063-14cb19ba19a9"));
            return Ok(_mapper.Map<List<TicketReportSummaryDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-email-ticket-report-summary/{ticketNo}")]
        public ActionResult<TicketEmailReportSummary> GetEmailTicketReportSummary(string ticketNo)
        {
            return Ok(_reportAgentService.GetEmailTicketReportSummaries(ticketNo));


        }
    }
}