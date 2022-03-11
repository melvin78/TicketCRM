using System.Collections.Generic;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace Centrino.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Enquiries:BaseEntity
    {
        
        public string EnquiryType { get; set; }
        
        
        public int Timeline { get; set; }


        public List<EnquiryCategory> EnquiryCategories { get; set; }
          


    }
}