using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IOrganizationService
    {
        Task<int> AddNewOrganisationAsync(OrganizationDTO organizationDto);

        Task<List<Organization>> GetOrganisationAsync();


        string FindOrganisationName(Guid? saccoid);
    }
}