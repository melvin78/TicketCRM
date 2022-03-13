using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddDepartmentAsync(DepartmentDTO departmentDto)
        {
            if ( departmentDto == null) return 0;

            var department = DepartmentFactory.CreateNewDepartment(departmentDto.DepartmentVal);
            
            return await _unitOfWork.Repository<Department>().AddAsync(department);
        }

        public async Task<List<Department>> GetDepartmentAsync()
        {
            return await _unitOfWork.Repository<Department>().GetAllAsync();
        }
    }
}