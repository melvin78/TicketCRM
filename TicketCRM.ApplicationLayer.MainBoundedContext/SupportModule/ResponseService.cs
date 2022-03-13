using AutoMapper;
using IdentityServerAspNetIdentity.Services;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class ResponseService : IResponseService
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;
        private readonly IPusherService _pusherService;
        private readonly IUnitOfWork _unitOfWork;
        private List<Response> _responses;
        private int roomCount;
        private readonly List<int> unreadCount;

        private List<UserInfo> UserInfo;

        public ResponseService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IPusherService pusherService,
            IApplicationUserService applicationUserService
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pusherService = pusherService;
            _applicationUserService = applicationUserService;
            UserInfo = new List<UserInfo>();
            _responses = new List<Response>();
            roomCount = 0;
            unreadCount = new List<int>();
        }

        public async Task<bool> AddResponseAsync(ResponseDTO responseDto)
        {
            if (responseDto == null) return false;


            var response = ResponseFactory.AddNewResponseFactory(
                responseDto.TicketNumber,
                responseDto.ResponseText,
                responseDto.From,
                responseDto.To,
                responseDto.Attachments,
                responseDto.IsRead = false);

            await _unitOfWork.Repository<Response>().AddAsync(response);

            await _pusherService.SendPusherNotification(new
            {
                responseText = responseDto.ResponseText,
                attachments = responseDto.Attachments,
                from = responseDto.From,
                to = responseDto.To,
                response.Id,
                ticketNumber = responseDto.TicketNumber
            }, $"chatFrom-{responseDto.TicketNumber}", "sentResponse", responseDto.SocketId);


            return true;
        }

        public async Task<List<ResponseDTO>> GetAllResponsesAsync(string ticketNo)
        {
            var responses = _unitOfWork.Repository<Response>().FindAll(new ResponseSpecification(ticketNo)).ToList();
            return _mapper.Map<List<ResponseDTO>>(responses);
        }

        public async Task<bool> MarkAsRead(Guid responseId)
        {
            var persisted = _unitOfWork.Repository<Response>().FindAll(new ResponseSpecificationByIdentity(responseId))
                .First();

            if (persisted != null)
            {
                var current = ResponseFactory.AddNewResponseFactory(
                    persisted.TicketNumber,
                    persisted.ResponseText,
                    persisted.From,
                    persisted.To,
                    persisted.Attachments,
                    persisted.IsRead = true);

                _unitOfWork.Repository<Response>().Merge(persisted, current);
                return true;
            }

            return false;
        }

       

        public List<RoomDTO> ConfigureRooms(Guid userId)
        {
            List<RoomDTO> roomDtos = new List<RoomDTO>();

            List<LastMessageDTO> lastMessage = new List<LastMessageDTO>();

            
            var res = _unitOfWork.Repository<Response>().FindAll(
                new ResponseFromSpecification(userId));
            
            
            
            _responses = res.DistinctBy(o => new {o.TicketNumber}).ToList();

            roomCount = _responses.Count;

            for (int i = 0; i < roomCount; i++)
            {
                int unreadCount = _responses.Count(o => o.IsRead == false);
            }

            for (int i = 0; i < roomCount; i++)
            {
                var userInformation = new List<UserInfo>();
                
             
                lastMessage.Add(new LastMessageDTO()
                {
                    content= _responses[i].ResponseText,
                    seen = false,
                    senderId = _responses[i].To.ToString(),
                    timeStamp = _responses[i].CreatedAt.ToShortDateString(),
                    userName = "test",


                });
            }

            for (int i = 0; i < roomCount; i++)
            {
                var userInformation = new List<UserInfo>();

                for (int j = 0; j < 2; j++)
                {
                    if (j==0)
                    {
                        var firstUserDetails = _applicationUserService.GetFirstName(_responses[i].From.ToString());
                    
                        userInformation.Add(new UserInfo()
                        {
                            _id = _responses[i].From,
                            username = firstUserDetails
                        });
                    }

                    if (j==1)
                    {
                        var secondUserDetails = _applicationUserService.GetFirstName(_responses[i].To.ToString());

                        userInformation.Add(new UserInfo()
                        {
                            _id = _responses[i].To,
                            username= secondUserDetails
                        });
                    }

                    
                }
                
                roomDtos.Add(new RoomDTO
                {
                    roomId = _responses[i].TicketNumber,
                    roomName = userInformation[i].username,
                    UnreadCount = 2,
                    LastMessage = lastMessage[i],
                    users = userInformation,
                    index = _responses[i].TicketNumber,
                    
                });
            }
          
            
            return roomDtos;
        }


        public async Task<List<ResponseDTO>> UnreadResponses(Guid toId)
        {
            var responses = _unitOfWork.Repository<Response>()
                .FindAll(new ResponseSpecificationUnread()
                    .And(new ResponseSpecificationByReceiver(toId)));

            return _mapper.Map<List<ResponseDTO>>(responses);
        }
    }
}