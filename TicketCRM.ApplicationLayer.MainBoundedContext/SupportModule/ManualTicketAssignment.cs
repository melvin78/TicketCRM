using AutoMapper;
using IdentityServerAspNetIdentity.EmailService;
using IdentityServerAspNetIdentity.Services;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.EmailAgent;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquiryCategorySpecification;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class ManualTicketAssignment : IManualTicketAssignment
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly IAgentService _agentService;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateResolver<EmailAgentViewModel> _emailTemplateResolver;
        private readonly IPusherService _pusherService;
        private readonly IRecentActivityService _recentActivityService;
        private readonly IInboxService _inboxService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly ILogger<ManualTicketAssignment> _logger;

        public ManualTicketAssignment(
            IUnitOfWork unitOfWork,
            ITicketService ticketService,
            IMapper mapper,
            IAgentService agentService,
            IEmailService emailService,
            IEmailTemplateResolver<EmailAgentViewModel> emailTemplateResolver,
            IPusherService pusherService,
            IRecentActivityService recentActivityService,
            IInboxService inboxService,
            IApplicationUserService applicationUserService,
            ILogger<ManualTicketAssignment> logger
        )
        {
            _unitOfWork = unitOfWork;
            _ticketService = ticketService;
            _mapper = mapper;
            _agentService = agentService;
            _emailService = emailService;
            _emailTemplateResolver = emailTemplateResolver;
            _pusherService = pusherService;
            _recentActivityService = recentActivityService;
            _inboxService = inboxService;
            _applicationUserService = applicationUserService;
            _logger = logger;
        }

        public List<Agent> GetAgentList()
        {
            return _unitOfWork.Repository<Agent>().FindAll(new UserAgentSpecification())
                .ToList();
        }

        public async Task<bool> AssignTicketManually(string ticketNo, string agentId)
        {
            var agent = _unitOfWork.Repository<Agent>()
                .FindAll(new UserAgentByIdSpecification(Guid.Parse((ReadOnlySpan<char>) agentId)))
                .First();

            _ticketService.AssignAgentToUser(ticketNo, Guid.Parse((ReadOnlySpan<char>) agentId));


            var ticketDetails = _unitOfWork.Repository<Ticket>()
                .FindAll(new TicketMessageSpecification(ticketNo))
                .First();

            var userNameFrom = _applicationUserService.GetEmail(ticketDetails.CustomerId.ToString());

            var userNameTo = _applicationUserService.GetEmail(agentId);


            var inboxDto = new InboxDTO
            {
                Index = ticketNo,
                TicketNumber = ticketNo,
                UnreadCount = 0,
                LastMessageContent = "",
                LastMessageDistributed = false,
                LastMessageNew = false,
                LastMessageSaved = false,
                LastMessageSeen = false,
                LastMessageTimestamp = null,
                LastMessageSenderId = Guid.Empty,
                LastMessageUserName = "",
                RoomUsersIdFrom = ticketDetails.CustomerId,
                RoomUsersIdTo = Guid.Parse(agentId),
                RoomUsersUserNameFrom = userNameFrom,
                RoomUsersUserNameTo = userNameTo,
                Avatar =
                    "https://centrino-cdn.fra1.digitaloceanspaces.com/support/%E2%80%94Pngtree%E2%80%94ticket_4606064.png"
            };


            _inboxService.AddNewInbox(inboxDto);

            string firstMessage = ticketDetails.FirstMessage;
            string createdOn = ticketDetails.CreatedAt.ToLongDateString();
            string enquiry = _unitOfWork.Repository<EnquiryCategory>()
                .FindAll(new EnquiryCategorySpecificationId(ticketDetails.EnquiryCategoryId))
                .FirstOrDefault()
                ?.EnquiryCategoryVal;
            string clientEmailAddress =
                _applicationUserService.GetEmail(ticketDetails.CustomerId.ToString());

            var inboxId = _inboxService.GetInboxId(ticketDetails.TicketNo).ToString();

            string url = $"https://agents.caprover.centrino.co.ke/chat/{ticketDetails.TicketNo}/{inboxId}";


            SendEmailNotification(
                ticketDetails.TicketNo,
                agent.Username,
                firstMessage,
                enquiry,
                createdOn,
                clientEmailAddress,
                url
            );
            return true;
        }

        public bool SendEmailNotification(string ticketNo,
            string receiverEmailAddress,
            string firstMessage,
            string enquiry,
            string createdOn,
            string clientEmailAddress,
            string url)
        {
            var ticketNoViewModel =
                new EmailAgentViewModel(ticketNo, firstMessage, enquiry, createdOn, clientEmailAddress, url);

            var mailviewDto = new MailViewModelDTO();

            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            mailviewDto.EmailTemplatePath = "/Views/Emails/EmailAgent/EmailAgent.cshtml";
            mailviewDto.LinkedResourceContentId = "header";

            var builder = _emailTemplateResolver.BuildEmailBodyAsync(mailviewDto, ticketNoViewModel).Result;


            _emailService.SendEmail(builder.ToMessageBody(), receiverEmailAddress, "Ticket Assignment");

            return true;
        }
    }
}