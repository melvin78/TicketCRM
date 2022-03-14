using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryCategoryController : ControllerBase
    {
        private readonly IEnquiryCategoryService _enquiryCategoryService;
        private readonly IEnquiriesService _enquiriesService;
        private readonly IMapper _mapper;

        public EnquiryCategoryController(IEnquiryCategoryService enquiryCategoryService,
            IEnquiriesService enquiriesService, IMapper mapper)
        {
            _enquiryCategoryService = enquiryCategoryService;
            _enquiriesService = enquiriesService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add-new-enquiry-category")]
        public async Task<ActionResult> AddNewEnquiryCategory(string enquiryType, string enquiryCategoryValue)
        {
            var enquiryIdFk = _enquiriesService.GetParentEntityId(enquiryType);

            var enquiryCategoryDto = new EnquiryCategoryDTO()
            {
                EnquiryId = enquiryIdFk,
                EnquiryCategoryVal = enquiryCategoryValue
            };

            await _enquiryCategoryService.AddNewEnquiryCategoryAsync(enquiryCategoryDto);


            return Ok(true);

        }

        [HttpGet]
        [Route("get-enquiry-category/{enquiryId}")]
        public async Task<ActionResult<List<EnquiryCategoryDTO>>> GetEnquiryCategoryById(Guid enquiryId)
        {
            var enquiryCategories = await _enquiryCategoryService.GetEnquiryCategoryById(enquiryId);

            return Ok(_mapper.Map<List<EnquiryCategoryDTO>>(enquiryCategories));
        }

        [HttpGet]
        [Route("get-enquiry-category-by-enquiry/{enquiryId}")]
        public async Task<ActionResult<List<EnquiryCategoryDTO>>> GetEnquiryCategoryByEnquiry(string enquiryId)
        {
            return Ok(await _enquiryCategoryService.GetEnquiryCategoryByEnquiry(enquiryId));
        }
    }
}