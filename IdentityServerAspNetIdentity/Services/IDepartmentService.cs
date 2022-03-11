using System.Collections.Generic;
using IdentityServerAspNetIdentity.Configuration;

namespace IdentityServerAspNetIdentity.Services
{
    public interface IDepartmentService
    {
        List<Department> GetListOfDepartments();
    }
}