using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IAssignTicketService _assignTicketService;
        private readonly IResponseService _responseService;
        private readonly IManualTicketAssignment _manualTicketAssignment;

        public TicketController(ITicketService ticketService,
            IAssignTicketService assignTicketService,
            IResponseService responseService,
            IManualTicketAssignment manualTicketAssignment)
        {
            _ticketService = ticketService;
            _assignTicketService = assignTicketService;
            _responseService = responseService;
            _manualTicketAssignment = manualTicketAssignment;
        }


        [HttpGet]
        [Route("work-on-ticket/{ticketNo}/{agentId}")]
        public ActionResult<bool> WorkOnTicket(string ticketNo, string agentId)
        {
            return Ok(_manualTicketAssignment.AssignTicketManually(ticketNo, agentId));
        }

        [HttpPost]
        [Route("create-ticket")]
        public async Task<ActionResult<TicketDetailsDTO>> SaveEnquiry(TicketDTO ticketDto)
        {
            var ticketNo = await _ticketService.AddNewTicket(ticketDto);


            return Ok(ticketNo);
        }

        [HttpPost]
        [Route("find-all-tickets")]
        public async Task<ActionResult<TicketDetailsDTO>> FindTicket(string ticketno)
        {
            var ticketNo = new TicketDetailsDTO()
            {
                TicketNo = ticketno
            };

            await _ticketService.FindTicketUserDetails(ticketNo);

            return Ok(await _ticketService.FindTicketUserDetails(ticketNo));
        }


        [HttpPost]
        [Route("mark-ticket-as-overdue")]
        public ActionResult<TicketDetailsDTO> MarKTicketAsOverdue()
        {
            _ticketService.MarkTicketsAsOverdue(CancellationToken.None);

            return Ok();
        }


        [HttpGet]
        [Route("get-first-message/{ticketNo}")]
        public async Task<ActionResult<Guid>> GetUnassignedTickets(string ticketNo)
        {
            return Ok(await _ticketService.GetFirstMessage(ticketNo));
        }


        [HttpPost]
        [Route("assign-ticket-to-agent")]
        public ActionResult<Guid> AssignTicketToAgent(string ticketNo, Guid agentId)
        {
            return Ok(_ticketService.AssignAgentToUser(ticketNo, agentId));
        }

        [HttpGet]
        [Route("tickets-assigned-to-agent/{agentId}")]
        public async Task<ActionResult<string>> TicketsAssignedToAgent(string agentId)
        {
            return Ok(await _ticketService.GetAllTicketsAssignedToAgent(Guid.Parse(agentId)));
        }

        [HttpGet]
        [Route("tickets-opened-by-user/{userId}")]
        public async Task<ActionResult<string>> TicketsOpenedByUser(string userId)
        {
            return Ok(await _ticketService.GetAllTicketsOpenedByUser(Guid.Parse(userId)));
        }

        [HttpPost]
        [Route("round-robin-assign-ticket")]
        public ActionResult<bool> AssignTicketToAgent(CancellationToken cancellationToken)
        {
            _assignTicketService.AssignRoundRobin(cancellationToken);

            return Ok(true);
        }


        [HttpGet]
        [Route("count-new-tickets")]
        public ActionResult<int> NewTickets()
        {
            return Ok(_ticketService.FindNewTickets());
        }

        [HttpGet]
        [Route("count-open-tickets")]
        public ActionResult<int> OpenTickets()
        {
            return Ok(_ticketService.FindOpenedTickets());
        }

        [HttpGet]
        [Route("count-reopened-tickets")]
        public ActionResult<int> ReOpenedTickets()
        {
            return Ok(_ticketService.FindReopenedTickets());
        }

        [HttpGet]
        [Route("count-overdue-tickets")]
        public ActionResult<int> OverdueTickets()
        {
            return Ok(_ticketService.FindOverdueTickets());
        }

        [HttpGet]
        [Route("count-closed-tickets")]
        public ActionResult<int> ClosedTickets()
        {
            return Ok(_ticketService.FindClosedTickets());
        }

        [HttpGet]
        [HttpGet]
        [Route("count-transferred-tickets")]
        public ActionResult<int> TransferredTickets()
        {
            return Ok(_ticketService.FindTransferredTickets());
        }

        [HttpGet]
        [Route("count-resolved-tickets")]
        public ActionResult<int> ResolvedTickets()
        {
            return Ok(_ticketService.FindResolvedTickets());
        }

        [HttpGet]
        [Route("count-organisation-new-tickets/{organisationId}")]
        public ActionResult<int> OrganizationNewTickets(string organisationId)
        {
            return Ok(_ticketService.FindOrganizationNewTickets(Guid.Parse(organisationId)));
        }

        [HttpGet]
        [Route("count-organization-reopened-tickets/{organizationId}")]
        public ActionResult<int> OrganizationReopenedTickets(string organizationId)
        {
            return Ok(_ticketService.FindOrganizationReopenedTickets(Guid.Parse(organizationId)));
        }

        [HttpGet]
        [Route("count-organization-opened-tickets/{organizationId}")]
        public ActionResult<int> OrganizationOpenedTickets(string organizationId)
        {
            return Ok(_ticketService.FindOrganizationOpenedTickets(Guid.Parse(organizationId)));
        }

        [HttpGet]
        [Route("count-organization-overdue-tickets/{organizationId}")]
        public ActionResult<int> OrganizationOverdueTickets(string organizationId)
        {
            return Ok(_ticketService.FindOrganizationOverdueTickets(Guid.Parse(organizationId)));
        }

        [HttpGet]
        [Route("count-organization-closed-tickets/{organizationId}")]
        public ActionResult<int> OrganizationClosedTickets(string organizationId)
        {
            return Ok(_ticketService.FindOrganizationClosedTickets(Guid.Parse(organizationId)));
        }

        [HttpGet]
        [Route("count-organization-transferred-tickets/{organizationId}")]
        public ActionResult<int> OrganizationTransferredTickets(string organizationId)
        {
            return Ok(_ticketService.FindOrganizationTransferredTickets(Guid.Parse(organizationId)));
        }

        [HttpGet]
        [Route("count-organization-resolved-tickets/{organizationId}")]
        public ActionResult<int> ResolvedTickets(string organizationId)
        {
            return Ok(_ticketService.FindOrganizationResolvedTickets(Guid.Parse(organizationId)));
        }

        [HttpGet]
        [Route("get-ticket-information/{customerId}")]
        public ActionResult<List<TicketInfoDTO>> UnassignedTickets(string customerId)
        {
            return Ok(_ticketService.GetAllTicketInformationByCustomerId(Guid.Parse(customerId)));
        }

        [HttpGet]
        [Route("get-resolved-ticket-information")]
        public ActionResult<List<TicketInfoDTO>> GetResolvedTicketInformation()
        {
            return Ok(_ticketService.GetResolvedTicketInformation());
        }

        [HttpGet]
        [Route("get-closed-ticket-information")]
        public ActionResult<List<TicketInfoDTO>> GetClosedTicketInformation()
        {
            return Ok(_ticketService.GetClosedTicketInformation());
        }




        [HttpGet]
        [Route("resolve-tickets/{ticketNo}")]
        public async Task<ActionResult<bool>> ResolveTicket(string ticketNo)
        {
            return Ok(await _ticketService.ResolveTicket(ticketNo));
        }

        [HttpGet]
        [Route("close-ticket/{ticketNo}")]
        public ActionResult<object> CloseTicket(string ticketNo)
        {
            return Ok(_ticketService.CloseTicket(ticketNo));
        }

        [HttpGet]
        [Route("re-open-ticket/{ticketNo}/{remarks}")]
        public ActionResult<object> ReOpenTicket(string ticketNo, string remarks)
        {
            return Ok(_ticketService.ReOpenTicket(ticketNo, remarks));
        }


        [HttpGet]
        [Route("transfer-ticket-to-agent/{ticketNo}/{agentId}")]
        public async Task<ActionResult<bool>> TransferTicketToAgent(string ticketNo, string agentId)
        {

            return Ok(await _ticketService.TransferTicket(ticketNo, agentId));
        }
    }
}