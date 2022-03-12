using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class DepartmentFactory
    {
        public static Department CreateNewDepartment(string departmentValue)
        {
            var department = new Department();
            
            department.GenerateNewIdentity();

            department.DepartmentVal = departmentValue;
            
            department.CreatedAt=DateTime.Now;
             

            return department;

        }
        
    }
}