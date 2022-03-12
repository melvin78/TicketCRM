using IdentityServerAspNetIdentity.Models;

namespace TicketCRM.DomainLayer.MainBoundedContext
{
    public interface IRepositoryIdentityServer
    {

        IEnumerable<ApplicationUser> Find(ISpecification<ApplicationUser> specification = null);
        

        int AllMatchingCount(ISpecification<ApplicationUser> specification);

    }
}