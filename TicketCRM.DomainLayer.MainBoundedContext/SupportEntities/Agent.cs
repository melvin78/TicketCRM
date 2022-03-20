namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Agent:BaseEntity
    {
        
        public Guid UserId { get; set; }
        
        public string Username { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public bool HasToken { get; set; }
        
        public Guid DepartmentId { get; set; }

        public DateTime? TokenAssignmentDate { get; set; }
        
        public string? Avatar { get; set; }
        
        public string? Role { get; set; }
        
    }
}