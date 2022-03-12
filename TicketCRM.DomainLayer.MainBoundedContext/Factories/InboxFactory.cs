using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContext.ValueObjects;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class InboxFactory
    {

        public static Inbox AddNewInbox(string index,string ticketNumber,int unreadCount, LastMessage lastMessage,RoomUsers roomUsers,string avatar)
        {
            var inbox = new Inbox();
                
            inbox.GenerateNewIdentity();


            inbox.Index = index;

            inbox.TicketNumber = ticketNumber;
            
            inbox.UnreadCount = unreadCount;

            inbox.LastMessage = lastMessage;

            inbox.Avatar = avatar;
            
            inbox.CreatedAt= DateTime.Now;

            inbox.RoomUsers = roomUsers;
            
            return inbox;


        }
        
    }
}