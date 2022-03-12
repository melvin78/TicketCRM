using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class AgentFactory
    {
        public static Agent CreateNewAgent(string userName,Guid departmentId, 
            string firstName,string secondName,Guid userId,DateTime? tokenAssignmentDate,bool HasToken)
        {
            var agent = new Agent();
            
            agent.Id = Guid.NewGuid();

            agent.Username = userName;
            
            agent.CreatedAt= DateTime.Now;

            agent.DepartmentId = departmentId;

            agent.FirstName = firstName;

            agent.HasToken = HasToken;

            agent.SecondName = secondName;

            agent.UserId = userId;

            agent.TokenAssignmentDate = tokenAssignmentDate;

            return agent;

        }
        
    }
}