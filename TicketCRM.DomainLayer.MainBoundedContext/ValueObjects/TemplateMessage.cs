using Domain.Seedwork;

namespace Centrino.DomainLayer.MainBoundedContext.ValueObjects
{
    public class TemplateMessage:ValueObject<TemplateMessage>
    {

        public TemplateMessage()
        {
            
        }
        public TemplateMessage(string colorCode, string logoLocation, string header, string contactDetails,string downloads)
        {
            ColorCode = colorCode;
            LogoLocation = logoLocation;
            Header = header;
            ContactDetails = contactDetails;
            Downloads = downloads;

        }
        public string ColorCode { get;  set; }
        
        public string LogoLocation { get;  set; }
        
        public string Header { get;  set; }
        
        
        public string ContactDetails { get;  set; }
        
        public string Downloads { get;  set; }
    }
    
    
}