using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{

    public static class EnquiriesFactory
    {

        public static Enquiries CreateNewEnquiry(string enquiryType)
        {

            var enquiries = new Enquiries();

            enquiries.GenerateNewIdentity();
            
            enquiries.EnquiryType = enquiryType;
            
            enquiries.CreatedAt= DateTime.Now;
            

            return enquiries;


        }
    }
}