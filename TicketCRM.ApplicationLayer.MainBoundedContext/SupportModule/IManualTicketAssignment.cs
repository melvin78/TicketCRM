namespace TicketCRM.SupportModule
{
    public interface IManualTicketAssignment
    {
        Task<bool> AssignTicketManually(string ticketNo,string agentId);
    }
}