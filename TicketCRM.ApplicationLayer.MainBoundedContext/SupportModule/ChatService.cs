using AutoMapper;
using IdentityServerAspNetIdentity.EmailService;
using IdentityServerAspNetIdentity.Services;
using Newtonsoft.Json;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.ConversationalEmails;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.ChatSpecifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContext.ValueObjects;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class ChatService:IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPusherService _pusherService;
        private readonly IMapper _mapper;
        private readonly IEmailTemplateResolver<ConversationalEmailViewModel> _conversationEmailViewModel;
        private readonly IEmailService _emailService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly ITicketService _ticketService;
 
        public ChatService(IUnitOfWork unitOfWork,
            IPusherService pusherService,IMapper mapper,
            IEmailTemplateResolver<ConversationalEmailViewModel> conversationEmailViewModel,
            IEmailService emailService, 
            IApplicationUserService applicationUserService,
            ITicketService ticketService
               )
        {
            _unitOfWork = unitOfWork;
            _pusherService = pusherService;
            _mapper = mapper;
            _conversationEmailViewModel = conversationEmailViewModel;
            _emailService = emailService;
            _applicationUserService = applicationUserService;
            _ticketService = ticketService;
         }
        public async Task<int> AddChatAsync(ChatDTO chatDto)
        {
            if (chatDto == null)
                return 0;

            var chatFile = new ChatFile(
                chatDto.ChatFileName,
                chatDto.ChatFileSize,
                chatDto.ChatFileType,
                chatDto.ChatFileAudio,
                chatDto.ChatFileDuration,
                chatDto.ChatFileUrl,
                chatDto.ChatFilePreview);

            var replyMessageChatFile = new ChatFile(
                chatDto.ReplyMessageChatFileName,
                chatDto.ReplyMessageChatFileSize,
                chatDto.ReplyMessageChatFileType,
                chatDto.ReplyMessageChatFileAudio,
                chatDto.ReplyMessageChatFileDuration,
                chatDto.ReplyMessageChatFileUrl,
                chatDto.ReplyMessageChatFilePreview);

            var replyMessage = new ReplyMessage(chatDto.ReplyMessageContent,chatDto.SenderId,replyMessageChatFile);

            var chat = ChatFactory.AddNewChat(
                chatDto.Avatar,
                chatDto.Content,
                chatDto.Deleted,
                chatDto.Distributed,
                chatDto.Saved,
                chatDto.Seen,
                chatDto.System,
                chatFile,
                chatDto.DisabledActions,
                chatDto.DisabledReactions,
                chatDto.IndexId,
                replyMessage,
                chatDto.SenderId,
                chatDto.TimeStamp,
                chatDto.UserName,
                chatDto.InboxId,
                chatDto.ChatId,
                chatDto.Date
                );
            //
            // var senderEmailAddress = await _applicationUserService.GetUserEmail(chatDto.SenderId.ToString());
            //
            // var receiverEmailAddress = await _applicationUserService.GetUserEmail(chatDto.ReceiverEmailAddress);


            // string url = "";
            //
            // if (chatDto.Agent)
            // {
            //      url = $"https://helpdesk.centrino.co.ke/chat/{chatDto.IndexId}/{chatDto.InboxId}";
            // }
            //
            // else
            // {
            //     url = $"https://agents.caprover.centrino.co.ke/chat/{chatDto.IndexId}/{chatDto.InboxId}";
            // }
            //
            //   
            //
            // await SendConversationEmail(chatDto.Content, chatDto.ChatFileUrl, senderEmailAddress,
            //     receiverEmailAddress, chatDto.IndexId, url);
            //


            await _pusherService.SendPusherNotification(new
            {
                _id = chatDto.ChatId,
                indexId = chatDto.IndexId,
                content = chatDto.Content,
                senderId = chatDto.SenderId,
                username = chatDto.UserName,
                avatar = chatDto.Avatar,
                date = chatDto.Date,
                timestamp = chatDto.TimeStamp,
                files = chatDto.ChatFileUrl==null? JsonConvert.DeserializeObject("[]"): JsonConvert.DeserializeObject(chatDto.ChatFileUrl) 
            }, $"ticket-{chatDto.InboxId}", "conversation",chatDto.SocketId);
            
            return await _unitOfWork.Repository<Chats>().AddAsync(chat);


        }

        public List<object> GetChatsAsync(string inboxId)
        {
            var res=  _unitOfWork.Repository<Chats>().FindAll(new ChatInboxSpecification(inboxId))
                .ToList();

            List<object> chats = new List<object>();
            
            for (int i = 0; i < res.Count; i++)
            {
                chats.Add(new
                {
                    
                    _id=res[i].ChatId,
                    indexId = res[i].IndexId,
                    content=res[i].Content,
                    senderId=res[i].SenderId,
                    username=res[i].UserName,
                    avatar=res[i].Avatar,
                    date=res[i].Date,
                    timestamp=res[i].TimeStamp,
                    files= res[i].ChatFile.Url==null? JsonConvert.DeserializeObject("[]"): JsonConvert.DeserializeObject(res[i].ChatFile.Url) 
                });
                
            }

            return chats;
        }

        public async Task<int> SendConversationEmail(string message, string attachments, string senderEmailAddress,string receiverEmailAddress, string ticketNumber, string url)
        {
            var supportViewModel = new ConversationalEmailViewModel(message,attachments,senderEmailAddress,ticketNumber,url);

            var mailviewDto = new MailViewModelDTO();

            
            
            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            
            mailviewDto.EmailTemplatePath = "/Views/Emails/ConversationalEmails/ConversationalEmail.cshtml";
            
            mailviewDto.LinkedResourceContentId = "header";
            
            var builder = await _conversationEmailViewModel.BuildEmailBodyAsync(mailviewDto, supportViewModel);


            await _emailService.SendEmailAsync(builder.ToMessageBody(), receiverEmailAddress, "New Message");

            return 1;
            
        }
    }
}