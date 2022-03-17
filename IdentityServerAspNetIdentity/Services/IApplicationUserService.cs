using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;

namespace IdentityServerAspNetIdentity.Services
{
    public interface IApplicationUserService
    {

        Task<string> GetUserEmail(string userId);

        string GetEmail(string userId);

        Guid? GetOrganizationId(string userId);
        
        string GetFirstName(string userId);

        ApplicationUser GetUserDetails(string userId);
        
        string GetSecondName(string userId);

        List<ApplicationUser> GetRegisteredUsers();


    }
}