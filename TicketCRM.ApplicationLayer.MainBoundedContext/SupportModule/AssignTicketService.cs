using AutoMapper;
using Humanizer;
using IdentityServerAspNetIdentity.EmailService;
using RoundRobin;
using IdentityServerAspNetIdentity.Services;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.EmailAgent;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquiryCategorySpecification;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.InboxSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class AssignTicketService : IAssignTicketService
    {
        private readonly List<Agent> _agentList;
        private readonly IAgentService _agentService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateResolver<EmailAgentViewModel> _emailTemplateResolver;
        private readonly IInboxService _inboxService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPusherService _pusherService;
        private readonly IRecentActivityService _recentActivityService;
        private readonly ITicketService _ticketService;
        private readonly RoundRobinList<Agent> _roundRobinList;

        private readonly List<TicketDetailsDTO> _unassignedTickets;

        private readonly IUnitOfWork _unitOfWork;

        public AssignTicketService(
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
            ILogger<AssignTicketService> logger
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
            _unassignedTickets = GetAllUnassignedTickets();
            _agentList = GetAgentList();
            _roundRobinList = SaveRoundRobinAgent();
        }


        public async Task<bool> SendEmailNotificationAsync(string ticketNo, string receiverEmailAddress)
        {
            // var ticketNoViewModel = new EmailAgentViewModel(ticketNo);

            var mailviewDto = new MailViewModelDTO();

            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            mailviewDto.EmailTemplatePath = "/Views/Emails/EmailAgent/EmailAgent.cshtml";
            mailviewDto.LinkedResourceContentId = "header";

            // var builder = await _emailTemplateResolver.BuildEmailBodyAsync(mailviewDto, ticketNoViewModel);


            // await _emailService.SendEmailAsync(builder.ToMessageBody(), receiverEmailAddress, "New Ticket");

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

        public List<TicketDetailsDTO> GetAllUnassignedTickets()
        {
            var unassignedTickets = _unitOfWork.Repository<Ticket>().FindAll(new UnattendedTicketSpecification());

            return _mapper.Map<List<TicketDetailsDTO>>(unassignedTickets);
        }

        public List<Agent> GetAgentList()
        {
            return _unitOfWork.Repository<Agent>().FindAll(new UserAgentSpecification())
                .ToList();
        }

        public RoundRobinList<Agent> SaveRoundRobinAgent()
        {
            return new RoundRobinList<Agent>(_agentList);
        }


        public void AssignRoundRobin(CancellationToken cancellationToken)
        {
            for (int i = 0; i < _agentList.Count; i++)
            {
                _logger.LogInformation(
                    $"Agent list is{_agentList[i].Username} token assignment date is {_agentList[i].TokenAssignmentDate.Humanize()} ");
            }


            for (int i = 0; i < _agentList.Count; i++)
            {
                _logger.LogInformation(
                    $"First agent is {_roundRobinList.Next().Username} and token assignment date is {_roundRobinList.Next().TokenAssignmentDate.Humanize()}");
            }


            for (var j = 0; j < _unassignedTickets.Count; j++)
            {
                var counterAgent = _roundRobinList.Next().UserId;
                
                _logger.LogInformation($"First agent is {_roundRobinList.Next().Username}");
                
                _ticketService.AssignAgentToUser(_unassignedTickets[j].TicketNo,
                    counterAgent);

                var userNameFrom = _applicationUserService.GetEmail(_unassignedTickets[j].CustomerId.ToString());

                var userNameTo = _applicationUserService
                    .GetEmail(counterAgent.ToString());


                var inboxDto = new InboxDTO
                {
                    Index = _unassignedTickets[j].TicketNo,
                    TicketNumber = _unassignedTickets[j].TicketNo,
                    UnreadCount = 0,
                    LastMessageContent = "",
                    LastMessageDistributed = false,
                    LastMessageNew = false,
                    LastMessageSaved = false,
                    LastMessageSeen = false,
                    LastMessageTimestamp = null,
                    LastMessageSenderId = Guid.Empty,
                    LastMessageUserName = "",
                    RoomUsersIdFrom = _unassignedTickets[j].CustomerId,
                    RoomUsersIdTo = counterAgent,
                    RoomUsersUserNameFrom = userNameFrom,
                    RoomUsersUserNameTo = userNameTo,
                    Avatar =
                        "https://centrino-cdn.fra1.digitaloceanspaces.com/support/%E2%80%94Pngtree%E2%80%94ticket_4606064.png"
                };


                _inboxService.AddNewInbox(inboxDto);


                if (j == _unassignedTickets.Count - 1)
                    _agentService.AssignAgentWithToken(_agentList.Last().UserId,
                        _agentList.First().UserId);
            }
        }

        public void SendEmail(CancellationToken cancellationToken)
        {
            var _roundRobinList = new RoundRobinList<Agent>(
                _agentList
            );
            for (var j = 0; j < _unassignedTickets.Count; j++)
            {
                var ticketDetails = _unitOfWork.Repository<Ticket>()
                    .FindAll(new TicketMessageSpecification(_unassignedTickets[j].TicketNo))
                    .FirstOrDefault();

                string firstMessage = ticketDetails.FirstMessage;
                string createdOn = ticketDetails.CreatedAt.ToLongDateString();
                string enquiry = _unitOfWork.Repository<EnquiryCategory>()
                    .FindAll(new EnquiryCategorySpecificationId(_unassignedTickets[j].EnquiryCategoryId))
                    .FirstOrDefault()
                    ?.EnquiryCategoryVal;
                string clientEmailAddress =
                    _applicationUserService.GetEmail(_unassignedTickets[j].CustomerId.ToString());

                var inboxId = _inboxService.GetInboxId(_unassignedTickets[j].TicketNo).ToString();

                string url = $"https://agents.caprover.centrino.co.ke/chat/{_unassignedTickets[j].TicketNo}/{inboxId}";


                SendEmailNotification(
                    _unassignedTickets[j].TicketNo,
                    _roundRobinList.Next().Username,
                    firstMessage,
                    enquiry,
                    createdOn,
                    clientEmailAddress,
                    url
                );
            }
        }

        public void MarkTicketsAsOverdue(CancellationToken cancellationToken)
        {
            var assignedTickets = _unitOfWork.Repository<Ticket>()
                .FindAll(new TicketAssignedSpecification().And(new TicketPastDueDateSpecification())).ToList();


            if (assignedTickets.Count != 0)
            {
                for (int i = 0; i < assignedTickets.Count; i++)
                {
                    //PESAPEPE
                    if (assignedTickets[i].EnquiryId == Guid.Parse("38a565a0-461c-ec11-b063-14cb19ba19a9")
                        && TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours >
                        2) ///should be 2 hours
                    {
                        _logger.LogInformation($"the ticket number is {assignedTickets[i].TicketNo} " +
                                               $"the ticket was created on{assignedTickets[i].CreatedAt}" +
                                               $" and the time now is ${DateTime.Now}" +
                                               $"subtraction = {TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours} ");

                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).FirstOrDefault();

                        if (inbox == null)
                        {
                            throw new ArgumentNullException(nameof(inbox),
                                $"{assignedTickets[i].TicketNo}missing inbox");
                        }


                        var url = $"https://agents.caprover.centrino.co.ke/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        _ticketService.MarkTicketAsPending(assignedTickets[i].TicketNo);

                        _ticketService.SendOverdueTicketEmail(assignedTickets[i].TicketNo,
                            assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);

                        _logger.LogInformation($" Pesapepe query is late email has been sent to {rUserEmail}");
                    }

                    //ATM BRIDGE
                    if (assignedTickets[i].EnquiryId == Guid.Parse("cdc702bd-461c-ec11-b063-14cb19ba19a9")
                        && TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours >
                        2) ///should be 2 hours
                    {
                        _logger.LogInformation($"the ticket number is {assignedTickets[i].TicketNo} " +
                                               $"the ticket was created on{assignedTickets[i].CreatedAt}" +
                                               $" and the time now is ${DateTime.Now}" +
                                               $"subtraction = {TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours} ");

                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).FirstOrDefault();

                        var url = $"https://agents.caprover.centrino.co.ke/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        _ticketService.MarkTicketAsPending(assignedTickets[i].TicketNo);

                        _ticketService.SendOverdueTicketEmail(assignedTickets[i].TicketNo,
                            assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);

                        _logger.LogInformation($" atm bridge query is late email has been sent to {rUserEmail}");
                    }


                    //BULK SMS
                    if (assignedTickets[i].EnquiryId == Guid.Parse("7811e72d-4168-4652-9263-a6ec1d4974bf")
                        && TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours >
                        24) //should be 24 hours
                    {
                        _logger.LogInformation($"the ticket number is {assignedTickets[i].TicketNo} " +
                                               $"the ticket was created on{assignedTickets[i].CreatedAt}" +
                                               $" and the time now is ${DateTime.Now}" +
                                               $"subtraction = {TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours} ");

                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).First();

                        var url = $"https://agents.caprover.centrino.co.ke/{assignedTickets[i].TicketNo}/{inbox.Id}";


                        _ticketService.MarkTicketAsPending(assignedTickets[i].TicketNo);

                        _ticketService.SendOverdueTicketEmail(assignedTickets[i].TicketNo,
                            assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);

                        _logger.LogInformation($" bulk sms query is late email has been sent to {rUserEmail}");
                    }

                    //VANGUARD FINANCIALS
                    if (assignedTickets[i].EnquiryId == Guid.Parse("bdd797b0-461c-ec11-b063-14cb19ba19a9")
                        && TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours >
                        24) //should be 24 hors
                    {
                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        _logger.LogInformation($"the ticket number is {assignedTickets[i].TicketNo} " +
                                               $"the ticket was created on{assignedTickets[i].CreatedAt}" +
                                               $" and the time now is ${DateTime.Now}" +
                                               $"subtraction = {TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours} ");

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).First();

                        var url = $"https://agents.caprover.centrino.co.ke/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        _ticketService.MarkTicketAsPending(assignedTickets[i].TicketNo);

                        _ticketService.SendOverdueTicketEmail(assignedTickets[i].TicketNo,
                            assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);

                        _logger.LogInformation(
                            $" vanguard financials query is late email has been sent to {rUserEmail}");
                    }

                    //MEMBERS PORTAL
                    if (assignedTickets[i].EnquiryId == Guid.Parse("b01e2bad-e787-4777-a2f6-8219e609c503")
                        && TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours >
                        24)
                    {
                        _logger.LogInformation($"the ticket number is {assignedTickets[i].TicketNo} " +
                                               $"the ticket was created on{assignedTickets[i].CreatedAt}" +
                                               $" and the time now is ${DateTime.Now}" +
                                               $"subtraction = {TimeSpan.FromTicks(DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Ticks).TotalHours} ");

                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).First();

                        var url = $"https://agents.caprover.centrino.co.ke/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        _ticketService.MarkTicketAsPending(assignedTickets[i].TicketNo);

                        _ticketService.SendOverdueTicketEmail(assignedTickets[i].TicketNo,
                            assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);

                        _logger.LogInformation($" members portal query is late email has been sent to {rUserEmail}");
                    }
                }
            }
        }

        public void SendPusher(CancellationToken cancellationToken)
        {
            var _roundRobinList = new RoundRobinList<Agent>(
                _agentList
            );

            for (var j = 0; j < _unassignedTickets.Count; j++)
                _pusherService.SendPusherNotification(new
                {
                    emailAddress = _roundRobinList.Next().Username,
                    ticketNo = _unassignedTickets[j].TicketNo,
                    task = "Ticket Assigned",
                    color = "#00838F",
                    icon = "mdi-human-greeting"
                }, "TicketAssigned", "AssignedTicket");
        }


        public void SaveActivity(CancellationToken cancellationToken)
        {
            var _roundRobinList = new RoundRobinList<Agent>(
                _agentList
            );
            for (var j = 0; j < _unassignedTickets.Count; j++)
            {
                var recentActivityDto = new RecentActivityDTO
                {
                    ticketNo = _unassignedTickets[j].TicketNo,
                    emailAddress = _roundRobinList.Next().Username,
                    color = "#00838F",
                    task = "Ticket Assigned",
                    icon = "mdi-human-greeting"
                };

                _recentActivityService.AddRecentActivity(recentActivityDto);
            }
        }

        public void CreateRoom(CancellationToken cancellationToken)
        {
            var _roundRobinList = new RoundRobinList<Agent>(
                _agentList
            );


            for (var j = 0; j < _unassignedTickets.Count; j++)
            {
                var userNameFrom = _applicationUserService.GetEmail(_unassignedTickets[j].CustomerId.ToString());

                var userNameTo = _applicationUserService
                    .GetEmail(_roundRobinList.Next().UserId.ToString());


                var inboxDto = new InboxDTO
                {
                    Index = _unassignedTickets[j].TicketNo,
                    TicketNumber = _unassignedTickets[j].TicketNo,
                    UnreadCount = 0,
                    LastMessageContent = "",
                    LastMessageDistributed = false,
                    LastMessageNew = false,
                    LastMessageSaved = false,
                    LastMessageSeen = false,
                    LastMessageTimestamp = null,
                    LastMessageSenderId = Guid.Empty,
                    LastMessageUserName = "",
                    RoomUsersIdFrom = _unassignedTickets[j].CustomerId,
                    RoomUsersIdTo = _roundRobinList.Next().UserId,
                    RoomUsersUserNameFrom = userNameFrom,
                    RoomUsersUserNameTo = userNameTo,
                    Avatar =
                        "https://centrino-cdn.fra1.digitaloceanspaces.com/support/%E2%80%94Pngtree%E2%80%94ticket_4606064.png"
                };


                _inboxService.AddNewInbox(inboxDto);
            }
        }
    }
}