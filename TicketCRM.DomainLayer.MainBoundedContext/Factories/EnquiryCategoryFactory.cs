using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class EnquiryCategoryFactory
    {
        public static EnquiryCategory CreateNewEnquiryCategory(string enquiryCategoryValue,Guid enquiryGuid)
        {
            var enquiryCategory = new EnquiryCategory();
            
            enquiryCategory.GenerateNewIdentity();
            
            enquiryCategory.EnquiryCategoryVal=enquiryCategoryValue;

            enquiryCategory.EnquiryId = enquiryGuid;
            
            enquiryCategory.CreatedAt=DateTime.Now;

            return enquiryCategory;



        }
    }
}