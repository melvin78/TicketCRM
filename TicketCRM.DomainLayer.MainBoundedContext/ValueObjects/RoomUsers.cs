using System;
using Domain.Seedwork;

namespace Centrino.DomainLayer.MainBoundedContext.ValueObjects
{
    public class RoomUsers:ValueObject<RoomUsers>
    {
        public Guid IdFrom { get; set; }
        
        public Guid IdTo { get; set; }
        
        public  string UserNameFrom { get; set; }
        
        public string UserNameTo { get; set; }
        
        
        public string AvatarFrom { get; set; }
        
        
        public string AvatarTo { get; set; }
        



        public RoomUsers(Guid idFrom,string userNameFrom,Guid idTo,string userNameTo,string avatarFrom,string avatarTo)
        {
            IdFrom = idFrom;

            UserNameFrom = userNameFrom;
            
            IdTo = idTo;

            UserNameTo = userNameTo;

            AvatarFrom = avatarFrom;

            AvatarTo = avatarTo;



        }

        public RoomUsers()
        {
            
        }
        
        
    }
}