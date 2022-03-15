namespace TicketCRM.Infrastructure.Utilities.Utils
{
    public static class TicketNumberGenerator
    {
        public static string GenerateTicketNumber(Guid enquiryCategoryId,Guid organizationId)
        {
            Random generator = new Random();
            int r = generator.Next(100000);
            string generatedTicketNo = r.ToString("D6");

            string firstFourEnquiryCategoryId = new string(enquiryCategoryId.ToString().ToUpper().Take(4).ToArray());
            string firstFourOrganizationId = new string(organizationId.ToString().ToUpper().Take(4).ToArray());

            return $"CRM-{firstFourOrganizationId}-{generatedTicketNo}-{firstFourEnquiryCategoryId}";

        }
    }
}