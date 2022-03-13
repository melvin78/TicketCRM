using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IDepartmentService
    {
        Task<int> AddDepartmentAsync(DepartmentDTO departmentDto);
        
        Task<List<Department>> GetDepartmentAsync();


    }
}