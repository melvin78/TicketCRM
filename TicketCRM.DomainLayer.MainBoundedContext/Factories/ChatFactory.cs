using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContext.ValueObjects;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class ChatFactory
    {
        public static Chats AddNewChat(
            string? avatar,
            string? content,
            bool? deleted,
            bool? distributed,
            bool? saved,
            bool? seen,
            bool? system,
            ChatFile chatFile,
            bool? disabledActions,
            bool? disabledReactions,
            string? indexId,
            ReplyMessage replyMessage,
            Guid? senderId,
            string? timestamp,
            string? userName,
            string? inboxId,
            string? chatId,
            string? date
        )
        {
            var chat = new Chats();

            chat.GenerateNewIdentity();

            chat.Avatar = avatar;

            chat.Content = content;

            chat.Deleted = deleted;

            chat.Distributed = distributed;

            chat.Saved = saved;

            chat.Seen = seen;

            chat.System = system;

            chat.ChatFile = chatFile;

            chat.DisabledActions = disabledActions;

            chat.DisabledReactions = disabledReactions;

            chat.IndexId = indexId;

            chat.ReplyMessage = replyMessage;

            chat.SenderId = senderId;

            chat.TimeStamp = timestamp;

            chat.UserName = userName;

            chat.CreatedAt = DateTime.Now;

            chat.InboxId = inboxId;

            chat.ChatId = chatId;

            chat.Date = date;

            return chat;
        }
    }
}