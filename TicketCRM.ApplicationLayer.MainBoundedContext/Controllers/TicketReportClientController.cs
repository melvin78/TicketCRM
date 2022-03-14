using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketReportClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportClientService _reportClientService;

        public TicketReportClientController(IMapper mapper,IReportClientService reportClientService)
        {
            _mapper = mapper;
            _reportClientService = reportClientService;
        }
        
        [HttpGet]
        [Route("get-ticket-teports")]
        public async Task<ActionResult<IEnumerable<TicketReportClientDTO>>> GetTicketClientReport()
        {
            var ticketClientReports = await _reportClientService.GetTicketReports();
            return Ok(_mapper.Map<List<TicketReportClientDTO>>(ticketClientReports));
          
        }
        
        [HttpGet]
        [Route("get-ticket-reports-by-ticket-no/{ticketno}")]
        public  ActionResult GetTicketClientReportByticketno(string ticketno)
        {
            var ticketReportbyTicketno = _reportClientService.GetTicketReportsByTicketNo(ticketno);
            return Ok( _mapper.Map<TicketReportClientDTO>(ticketReportbyTicketno));
          
        }
        
               
        [HttpGet]
        [Route("get-ticket-reports-by-user-id/{userId}")]
        public ActionResult GetTicketClientReportByUserId(string userId)
        {
            var ticketReportbyUserId = _reportClientService.GetAllTicketReportsByUserId(Guid.Parse(userId));
            return Ok(_mapper.Map<List<TicketReportClientDTO>>(ticketReportbyUserId));
          
        }
    }
}