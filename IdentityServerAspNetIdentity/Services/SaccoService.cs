using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServerAspNetIdentity.Configuration;
using IdentityServerAspNetIdentity.Data;

namespace IdentityServerAspNetIdentity.Services
{
    public class SaccoService:ISaccoService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SaccoService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public List<Sacco> GetListOfSaccos()
        {
            return _applicationDbContext.Set<Sacco>().ToList();
        }

      
    }
}