using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IInboxService
    {
        int AddNewInbox(InboxDTO inboxDto);

        bool UpdateInbox(InboxDTO inboxDto);

        SingleRoomDTO GetInbox(string ticketNo);

        Guid GetInboxId(string ticketNo);

        List<RoomDTO> GetInboxes(Guid userId);
    }
}