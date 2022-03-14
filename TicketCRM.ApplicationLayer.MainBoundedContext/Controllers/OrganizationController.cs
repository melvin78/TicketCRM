using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        [Route("add-new-organization")]
        public async Task<ActionResult<bool>> AddNewOrganization(string value)
        {
            var organizationDto = new OrganizationDTO()
            {
                OrganizationName = value
            };

            await _organizationService.AddNewOrganisationAsync(organizationDto);


            return true;
        }

        [HttpGet]
        [Route("get-all-organization")]
        public async Task<ActionResult<IEnumerable<OrganizationDTO>>> GetAllSaccos()
        {
            return Ok(await _organizationService.GetOrganisationAsync());
        }
    }
}