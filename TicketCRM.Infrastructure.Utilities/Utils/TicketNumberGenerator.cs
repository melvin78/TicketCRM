using System;
using System.Linq;
using System.Security.Cryptography;

namespace Centrino.Infrastructure.Utils.Utils
{
    public static class TicketNumberGenerator
    {
        public static string GenerateTicketNumber(Guid enquiryCategoryId,Guid saccoId)
        {
            Random generator = new Random();
            int r = generator.Next(100000);
            string generatedTicketNo = r.ToString("D6");

            string firstTwoCharEnquiryCategory = new string(enquiryCategoryId.ToString().ToUpper().Take(4).ToArray());
            string firstTwoSacco = new string(saccoId.ToString().ToUpper().Take(4).ToArray());

            return $"CEN-{firstTwoSacco}-{generatedTicketNo}-{firstTwoCharEnquiryCategory}";

        }
    }
}