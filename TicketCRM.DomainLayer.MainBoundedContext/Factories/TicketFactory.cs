using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class TicketFactory
    {
        public static Ticket CreateNewTicket(
            Guid customerId, Guid enquiryCategoryId, Guid organizationId, Guid? careTaker,
            string attachments, string ticketNo, DateTime? resolvedOn,
            string firstMessage, Guid ticketStatusId, int priority, string remarks,
            DateTime? closedOn,
            DateTime? cancelledOn,
            DateTime? transferredOn,
            DateTime? assignedOn,
            DateTime? reopenedOn,
            DateTime? pastDueDate,
            Guid EnquiryId,
            DateTime? expectedDueDate
        )
        {
            var ticket = new Ticket();

            ticket.GenerateNewIdentity();

            ticket.TicketNo = ticketNo;

            ticket.OrganizationId = organizationId;

            ticket.TicketStatusId = ticketStatusId;

            ticket.Attachments = attachments;

            ticket.CareTaker = careTaker;

            ticket.PriorityLevel = priority;


            ticket.EnquiryCategoryId = enquiryCategoryId;

            ticket.ResolvedOn = resolvedOn;

            ticket.CustomerId = customerId;

            ticket.CreatedAt = DateTime.Now;

            ticket.FirstMessage = firstMessage;

            ticket.Remarks = remarks;

            ticket.ClosedOn = closedOn;

            ticket.ReopenedOn = reopenedOn;

            ticket.CancelledOn = cancelledOn;

            ticket.TransferredOn = transferredOn;

            ticket.AssignedOn = assignedOn;

            ticket.PastDueDate = pastDueDate;

            ticket.EnquiryId = EnquiryId;

            ticket.ExpectedDueDate = expectedDueDate;

            return ticket;
        }
    }
}