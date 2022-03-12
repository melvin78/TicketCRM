using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class SaccoFactory
    {

        public static Sacco CreateNewSacco(string saccoName)
        {
            var sacco = new Sacco();
            
            sacco.GenerateNewIdentity();

            sacco.SaccoName = saccoName;
            
            sacco.CreatedAt= DateTime.Now;
            

            return sacco;

        }
        
    }
}