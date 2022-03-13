using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IEnquiriesService
    {
        Task<int> AddNewEnquiryAsync(EnquiriesDTO enquiriesDto);
        
        Task<List<Enquiries>> GetEnquiriesAsync();

        Guid GetParentEntityId(string expression);
    }
}