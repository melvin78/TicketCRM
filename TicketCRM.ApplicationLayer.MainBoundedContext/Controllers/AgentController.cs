using AutoMapper;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.Services;
using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;
using AgentDTO = IdentityServerAspNetIdentity.Configuration.AgentDTO;
using IAgentService = TicketCRM.SupportModule.IAgentService;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;
        private readonly ITicketAssignmentService _ticketAssignmentService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public AgentController(IAgentService agentService,
            ITicketAssignmentService ticketAssignmentService,
            IApplicationUserService applicationUserService,
            IMapper mapper)
        {
            _agentService = agentService;
            _ticketAssignmentService = ticketAssignmentService;
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("get-ticket-assignment-report-by-ticketNo/{ticketno}")]
        public IActionResult GetTicketAssignmentReportByTicketNo(string ticketno)
        {
            var ticketReportbyTicketno = _ticketAssignmentService.GetAssignedTicketReportByTicketNo(ticketno);

            return Ok(_mapper.Map<TicketAssignmentDTO>(ticketReportbyTicketno));
        }

        [HttpGet]
        [Route("get-ticket-assignment-reports")]
        public async Task<ActionResult> GetAllTicketsAssigned()
        {
            var ticketAssignmentReport = await _ticketAssignmentService.GetAllAssignedTickets();

            return Ok(_mapper.Map<List<TicketAssignmentDTO>>(ticketAssignmentReport));
        }

        [HttpGet]
        [Route("get-ticket-assignment-reports-by-agent-id/{agentId}")]
        public async Task<ActionResult> GetTicketClientReport(string agentId)
        {
            var ticketAssignmentReport =
                await _ticketAssignmentService.GetAssignedTicketReportByAgentId(Guid.Parse(agentId));

            return Ok(_mapper.Map<List<TicketAssignmentDTO>>(ticketAssignmentReport));
        }

        [HttpGet]
        [Route("get-agent-details/{agentId}")]
        public async Task<ActionResult> GetAgentDetails(string agentId)
        {
            return Ok(await _agentService.GetAgentDetails(Guid.Parse(agentId)));
        }

        [HttpGet]
        [Route("get-agents")]
        public async Task<ActionResult> GetAllAgentDetails()
        {
            return Ok(await _agentService.GetListOfAgents());
        }

        [HttpGet]
        [Route("get-registered-users")]
        public ActionResult GetAllRegisteredUsers()
        {
            return Ok(_applicationUserService.GetRegisteredUsers());
        }
    }
}