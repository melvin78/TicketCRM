using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServerAspNetIdentity.Configuration;
using IdentityServerAspNetIdentity.Data;

namespace IdentityServerAspNetIdentity.Services
{
    public class OrganizationService:IOrganizationService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrganizationService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public List<Organization> GetListOfOrganizations()
        {
            return _applicationDbContext.Set<Organization>().ToList();
        }

      
    }
}