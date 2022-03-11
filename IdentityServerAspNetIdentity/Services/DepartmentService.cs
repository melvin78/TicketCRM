using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IdentityServerAspNetIdentity.Configuration;
using IdentityServerAspNetIdentity.Data;

namespace IdentityServerAspNetIdentity.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DepartmentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public List<Department> GetListOfDepartments()
        {
            return _applicationDbContext.Set<Department>().ToList();
        }
    }
}