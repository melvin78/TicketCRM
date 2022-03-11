using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServerAspNetIdentity.Profile
{
    public interface ICustomProfileService
    {
        Task GetProfileDataAsync(ProfileDataRequestContext context);

        Task IsActiveAsync(IsActiveContext context);
    }
}