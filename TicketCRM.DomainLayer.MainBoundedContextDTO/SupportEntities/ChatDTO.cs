namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class ChatDTO
    {
        public string ChatId { get; set; }
        
        public string Date { get; set; }
        
        public string SocketId { get; set; }
        
        public string IndexId { get; set; }
        
        public string Content { get; set; }
        
        
        public string InboxId { get; set; }

        public Guid SenderId { get; set; }
        
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string TimeStamp { get; set; }
        
        public bool System { get; set; }

        public bool Saved { get; set; }

        public bool Distributed { get; set; }

        public bool Seen { get; set; }
        
        public bool Deleted { get; set; }
        
        public bool DisabledActions { get; set; }
        
        public bool DisabledReactions { get; set; }
        
        public string ChatFileName { get; set; }
        
        public int ChatFileSize { get; set; }
        
        public  string ChatFileType { get; set; }
        
        public string ChatFileAudio{ get; set; }

        public int ChatFileDuration { get; set; }
        
        public string ChatFileUrl { get; set; }

        public string ChatFilePreview { get; set; }
        
        public string ReplyMessageContent { get; set; }

        public Guid ReplyMessageSenderId { get; set; }

        public string ReplyMessageChatFileName { get; set; }
        
        public int ReplyMessageChatFileSize { get; set; }
        
        public  string ReplyMessageChatFileType { get; set; }
        
        public string ReplyMessageChatFileAudio{ get; set; }

        public int ReplyMessageChatFileDuration { get; set; }
        
        public string ReplyMessageChatFileUrl { get; set; }

        public string ReplyMessageChatFilePreview { get; set; }
        
        public string ReceiverEmailAddress { get; set; }
        
        
        public bool Agent { get; set; }


    }
}