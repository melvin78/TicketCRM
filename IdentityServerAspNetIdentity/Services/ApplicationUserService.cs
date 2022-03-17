using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAspNetIdentity.Services
{
    public class ApplicationUserService:IApplicationUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ApplicationUserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<string> GetUserEmail(string userId)
        {
            return await _applicationDbContext.Set<ApplicationUser>()
                .Where(o => o.Id == userId)
                .Select(o => o.Email).FirstAsync();
        }

        public string GetEmail(string userId)
        {
            string t =  _applicationDbContext.Set<ApplicationUser>()
                .Where(o => o.Id == userId)
                .Select(o => o.Email).FirstOrDefault();

            if (t==null)
            {
                throw new ArgumentNullException(nameof(t), $"missing email{userId}");
            }

            return t;

        }

        public Guid? GetOrganizationId(string userId)
        {
            return  _applicationDbContext.Set<ApplicationUser>()
                .Where(o => o.Id == userId)
                .Select(o =>o.OrganizationId).Single();
        }

        public string GetFirstName(string userId)
        {
            return  _applicationDbContext.Set<ApplicationUser>()
                .Where(o => o.Id == userId)
                .Select(o =>o.FirstName).Single();
        }

        public ApplicationUser GetUserDetails(string userId)
        {
            return _applicationDbContext
                .Set<ApplicationUser>().First(o => o.Id == userId);
        }

        public string GetSecondName(string userId)
        {
            return  _applicationDbContext.Set<ApplicationUser>()
                .Where(o => o.Id == userId)
                .Select(o =>o.SecondName).Single();
        }

        public List<ApplicationUser> GetRegisteredUsers()
        {
            return _applicationDbContext.Set<ApplicationUser>().ToList();
        }
    }
}