using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class OrganizationFactory
    {

        public static Organization CreateNewOrganization(string organisationName)
        {
            var organization = new Organization();
            
            organization.GenerateNewIdentity();

            organization.OrganizationName = organisationName;
            
            organization.CreatedAt= DateTime.Now;
            

            return organization;

        }
        
    }
}