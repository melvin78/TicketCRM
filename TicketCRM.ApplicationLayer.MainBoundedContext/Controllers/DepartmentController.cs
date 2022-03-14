using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("add-new-department")]
        public async Task<ActionResult> AddNewDepartments(string deptValue)
        {
            var departmentDto = new DepartmentDTO
            {
                DepartmentVal = deptValue
            };

            await _departmentService.AddDepartmentAsync(departmentDto);

            return Ok(true);

        }

        [HttpGet]
        [Route("get-all-departments")]
        public async Task<ActionResult> GetProducts()
        {
            return Ok(await _departmentService.GetDepartmentAsync());

        }

    }
}
