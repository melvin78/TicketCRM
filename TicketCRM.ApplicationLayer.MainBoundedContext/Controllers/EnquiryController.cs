using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiriesService _enquiriesService;
        private readonly IMapper _mapper;

        public EnquiryController(IEnquiriesService enquiriesService,IMapper mapper)
        {
            _enquiriesService = enquiriesService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("add-new-enquiry")]
        public async Task<ActionResult<bool>> AddNewEnquiry(string value)
        {
            var enquiryDto = new EnquiriesDTO()
            {
                EnquiryType = value
            };

            await _enquiriesService.AddNewEnquiryAsync(enquiryDto);

            return true;
        }
        
        [HttpGet]
        [Route("get-enquiries")]
        public async Task<ActionResult<IEnumerable<EnquiriesDTO>>> GetEnquiries()
        {
            var enquiries = await _enquiriesService.GetEnquiriesAsync();
            return Ok(_mapper.Map<IEnumerable<EnquiriesDTO>>(enquiries));
          
        }
    }
}