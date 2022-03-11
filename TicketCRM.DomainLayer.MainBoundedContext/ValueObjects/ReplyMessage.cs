using System;
using Domain.Seedwork;

namespace Centrino.DomainLayer.MainBoundedContext.ValueObjects
{
    public class ReplyMessage : ValueObject<ReplyMessage>
    {
        public string Content { get; set; }

        public Guid SenderId { get; set; }

        public ChatFile ChatFile { get; set; }

        public ReplyMessage(string content,Guid senderId, ChatFile chatFile)
        {
            Content = content;

            SenderId = senderId;

            ChatFile = chatFile;

        }

        public ReplyMessage()
        {
            
        }


    }
}