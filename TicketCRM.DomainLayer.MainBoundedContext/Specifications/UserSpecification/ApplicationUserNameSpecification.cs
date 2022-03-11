using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using IdentityServerAspNetIdentity.Models;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification
{
    public class ApplicationUserNameSpecification : BaseSpecification<ApplicationUser>
    {
        public ApplicationUserNameSpecification(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }

        public override Expression<Func<ApplicationUser, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == UserId.ToString();
            }
        }
    }
}