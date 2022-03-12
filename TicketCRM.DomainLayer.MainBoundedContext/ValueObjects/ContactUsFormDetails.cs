namespace TicketCRM.DomainLayer.MainBoundedContext.ValueObjects
{
    public class ContactUsFormDetails: ValueObject<ContactUsFormDetails>
    {

    
        public ContactUsFormDetails(string name, string emailAddress, string message)
        {
            Name = name;
            EmailAddress = emailAddress;
            Message = message;
        }

        public ContactUsFormDetails()
        {
        }

        public string Name { get; set ; }

        public string EmailAddress { get; set; }

        public string Message { get; set; }
    }
}