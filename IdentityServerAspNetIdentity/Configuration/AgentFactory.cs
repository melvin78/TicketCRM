using System;

namespace IdentityServerAspNetIdentity.Configuration
{
    public static class AgentFactory
    {
        public static Agent CreateNewAgent(string userName,Guid departmentId, 
            string firstName,string secondName,Guid userId,DateTime? tokenAssignmentDate)
        {
            var agent = new Agent();
            
            agent.Id = Guid.NewGuid();

            agent.Username = userName;
            
            agent.CreatedAt= DateTime.Now;

            agent.DepartmentId = departmentId;

            agent.FirstName = firstName;

            agent.HasToken = false;

            agent.OrganizationId = Guid.Parse("3d18bfa3-0991-4c13-a866-86635d7863be");

            agent.SecondName = secondName;

            agent.UserId = userId;
            
            agent.TokenAssignmentDate = tokenAssignmentDate;


            return agent;

        }
        
    }
}