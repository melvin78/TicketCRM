using AutoMapper;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.InboxSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContext.ValueObjects;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class InboxService : IInboxService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InboxService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int AddNewInbox(InboxDTO inboxDto)
        {
            if (inboxDto == null) return 0;

            var roomUsers = new RoomUsers(inboxDto.RoomUsersIdFrom,
                inboxDto.RoomUsersUserNameFrom,
                inboxDto.RoomUsersIdTo, inboxDto.RoomUsersUserNameTo
                ,inboxDto.RoomUsersAvatarFrom,
                inboxDto.RoomUsersAvatarTo);

            var lastMessage = new LastMessage(inboxDto.LastMessageContent, inboxDto.LastMessageSenderId,
                inboxDto.LastMessageUserName, inboxDto.LastMessageTimestamp, inboxDto.LastMessageSaved,
                inboxDto.LastMessageDistributed, inboxDto.LastMessageSeen, inboxDto.LastMessageNew);

            var inbox = InboxFactory.AddNewInbox(inboxDto.Index, inboxDto.TicketNumber,
                inboxDto.UnreadCount, lastMessage, roomUsers,inboxDto.Avatar);


            return _unitOfWork.Repository<Inbox>().Add(inbox);
        }

        public bool UpdateInbox(InboxDTO inboxDto)
        {
            var persisted = _unitOfWork.Repository<Inbox>()
                .FindAll(new InboxBaseSpecification(inboxDto.Id)).First();


            if (persisted != null)
            {
                var lastMessage = new LastMessage(
                    inboxDto.LastMessageContent,
                    inboxDto.LastMessageSenderId,
                    inboxDto.LastMessageUserName,
                    inboxDto.LastMessageTimestamp,
                    inboxDto.LastMessageSaved,
                    inboxDto.LastMessageDistributed,
                    inboxDto.LastMessageSeen,
                    inboxDto.LastMessageNew);

                var roomUsers = new RoomUsers(
                    inboxDto.RoomUsersIdFrom,
                    inboxDto.RoomUsersUserNameFrom,
                    inboxDto.RoomUsersIdTo,
                    inboxDto.RoomUsersUserNameTo,
                    inboxDto.RoomUsersAvatarFrom,
                    inboxDto.RoomUsersAvatarTo);

                var current = InboxFactory.AddNewInbox(
                    persisted.Index,
                    persisted.TicketNumber,
                    persisted.UnreadCount,
                    persisted.LastMessage = lastMessage,
                    persisted.RoomUsers = roomUsers,
                    persisted.Avatar
                );

                _unitOfWork.Repository<Inbox>().Merge(persisted, current);
                return true;
            }

            return false;
        }

        public SingleRoomDTO GetInbox(string ticketNo)
        {
            var res = _unitOfWork.Repository<Inbox>().FindAll(new InboxTicketSpecification(ticketNo)).First();
            return _mapper.Map<SingleRoomDTO>(res);
        }

        public Guid GetInboxId(string ticketNo)
        {
            return _unitOfWork.Repository<Inbox>().FindAll(new InboxTicketSpecification(ticketNo)).First().Id;
        }

        public List<RoomDTO> GetInboxes(Guid userId)
        {
            var roomDtos = new List<RoomDTO>();


            var lastMessageDtos = new List<LastMessageDTO>();


            var res = _unitOfWork.Repository<Inbox>()
                .FindAll(new InboxFromSpecification(userId)
                    .Or(new InboxToSpecification(userId))).ToList();
            
            
            

            for (var i = 0; i < res.Count; i++)
                lastMessageDtos.Add(new LastMessageDTO
                {
                    content = res[i].LastMessage.Content,
                    seen = res[i].LastMessage.Seen,
                    timeStamp = res[i].LastMessage.Timestamp.ToString(),
                    senderId = res[i].LastMessage.SenderId.ToString(),
                    userName = res[i].LastMessage.UserName
                });

            for (var i = 0; i < res.Count; i++)
            {
                var userInfos = new List<UserInfo>();

                for (var j = 0; j < 2; j++)
                {
                    if (j == 0)
                        userInfos.Add(new UserInfo
                        {
                            _id = res[i].RoomUsers.IdFrom,
                            username = res[i].RoomUsers.UserNameFrom,
                            avatar = res[i].RoomUsers.AvatarFrom,
                        });

                    if (j == 1)
                        userInfos.Add(new UserInfo
                        {
                            _id = res[i].RoomUsers.IdTo,
                            username = res[i].RoomUsers.UserNameTo,
                            avatar = res[i].RoomUsers.AvatarTo
                        });
                }
                roomDtos.Add(new RoomDTO
                {
                    index = res[i].Index,
                    roomId = res[i].TicketNumber,
                    roomName = res[i].TicketNumber,
                    avatar = res[i].Avatar,
                    UnreadCount = res[i].UnreadCount,
                    users = userInfos,
                    LastMessage = lastMessageDtos[i]
                });

            }
                

            return roomDtos;
        }
    }
}