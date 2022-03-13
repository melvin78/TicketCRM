using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IEnquiryCategoryService
    {
        Task<int> AddNewEnquiryCategoryAsync(EnquiryCategoryDTO enquiryCategoryDto);

        string GetEnquiryCategory(Guid enquiryCategory);

        Task<List<EnquiryCategory>> GetEnquiryCategoryAsync();

        Task<List<EnquiryCategory>> GetEnquiryCategoryById(Guid enquiryCategoryId);

        Task<List<EnquiryCategory>> GetEnquiryCategoryByEnquiry(string enquiryCategoryVal);
    }
}