using System;
using System.Collections.Generic;
using IdentityServerAspNetIdentity.Configuration;

namespace IdentityServerAspNetIdentity.Services
{
    public interface IOrganizationService
    {
        List<Organization> GetListOfOrganizations();


    }
}