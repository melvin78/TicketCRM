using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class PriorityFactory
    {
        public static Priority AddNewPriority(string description,int level)
        {
            var priority = new Priority();
            
            priority.GenerateNewIdentity();
            
            priority.CreatedAt=DateTime.Now;

            priority.Description = description;

            priority.Level = level;

            return priority;
        }
        
    }
}