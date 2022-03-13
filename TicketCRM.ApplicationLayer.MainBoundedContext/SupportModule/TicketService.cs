using AutoMapper;
using IdentityServerAspNetIdentity.EmailService;
using IdentityServerAspNetIdentity.Services;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.OverdueTicket;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.ReminderEmail;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.ResolvedTicket;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.SupportEmail;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.TicketEmails;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.TransferredTicket;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.InboxSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSummarySpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.Infrastructure.Utilities.Utils;

namespace TicketCRM.SupportModule
{
    public class TicketService : ITicketService
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateResolver<TicketNumberViewModel> _emailTemplateResolver;
        private readonly IEnquiryCategoryService _enquiryCategoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<TicketService> _logger;
        private readonly IPusherService _pusherService;
        private readonly ISaccoTicketService _saccoTicketService;
        private readonly ISaccoService _saccoService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IRecentActivityService _recentActivityService;
        private readonly IEmailTemplateResolver<ResolvedTicketViewModel> _resolvedTicketViewModel;
        private readonly IEmailTemplateResolver<TransferredTicketViewModel> _transferredTicketViewModel;
        private readonly IEmailTemplateResolver<SupportEmailViewModel> _supportEmailViewModel;
        private readonly IEmailTemplateResolver<OverdueTicketModel> _overdueTicketEmailViewModel;
        private readonly IEmailTemplateResolver<ReminderViewModel> _reminderEmailTemplateResolver;
        

        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<TicketService> logger,
            IEmailTemplateResolver<TicketNumberViewModel> emailTemplateResolver,
            IEmailTemplateResolver<ResolvedTicketViewModel> resolvedTicketViewModel,
            IEmailTemplateResolver<TransferredTicketViewModel> transferredTicketViewModel,
            IEmailTemplateResolver<SupportEmailViewModel> supportEmailViewModel,
            IEmailTemplateResolver<OverdueTicketModel> overdueTicketEmailViewModel,
            IEmailTemplateResolver<ReminderViewModel> reminderEmailTemplateResolver,
            IEmailService emailService,
            IRecentActivityService recentActivityService,
            IApplicationUserService applicationUserService,
            IPusherService pusherService,
            ISaccoTicketService saccoTicketService,
            IWebHostEnvironment env,
            IConfiguration configuration,
            IEnquiryCategoryService enquiryCategoryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _emailTemplateResolver = emailTemplateResolver;
            _resolvedTicketViewModel = resolvedTicketViewModel;
            _transferredTicketViewModel = transferredTicketViewModel;
            _supportEmailViewModel = supportEmailViewModel;
            _overdueTicketEmailViewModel = overdueTicketEmailViewModel;
            _reminderEmailTemplateResolver = reminderEmailTemplateResolver;
            _emailService = emailService;
            _recentActivityService = recentActivityService;
            _applicationUserService = applicationUserService;
            _pusherService = pusherService;
            _saccoTicketService = saccoTicketService;
            _env = env;
            _configuration = configuration;
            _enquiryCategoryService = enquiryCategoryService;
        }

        public async Task<TicketDetailsDTO> AddNewTicket(TicketDTO ticketDto)
        {
            if (ticketDto == null) return null;

            if (ticketDto.EnquiryId == Guid.Parse("38a565a0-461c-ec11-b063-14cb19ba19a9")) //pesapepe
            {
                ticketDto.ExpectedDueDate = DateTime.Now.AddHours(2);
            }

            if (ticketDto.EnquiryId == Guid.Parse("cdc702bd-461c-ec11-b063-14cb19ba19a9")) //atm
            {
                ticketDto.ExpectedDueDate = DateTime.Now.AddHours(2);
            }

            if (ticketDto.EnquiryId == Guid.Parse("7811e72d-4168-4652-9263-a6ec1d4974bf")) //bulk sms
            {
                ticketDto.ExpectedDueDate = DateTime.Now.AddHours(24);
            }

            if (ticketDto.EnquiryId == Guid.Parse("bdd797b0-461c-ec11-b063-14cb19ba19a9")) //vanguard financials
            {
                ticketDto.ExpectedDueDate = DateTime.Now.AddHours(24);
            }

            if (ticketDto.EnquiryId == Guid.Parse("b01e2bad-e787-4777-a2f6-8219e609c503")) //members portal
            {
                ticketDto.ExpectedDueDate = DateTime.Now.AddHours(24);
            }


            var ticket = TicketFactory.CreateNewTicket(
                ticketDto.CustomerId,
                ticketDto.EnquiryCategoryId,
                ticketDto.SaccoId,
                ticketDto.CareTaker = Guid.Empty,
                ticketDto.Attachments,
                ticketDto.TicketNo =
                    TicketNumberGenerator.GenerateTicketNumber(ticketDto.EnquiryCategoryId, ticketDto.SaccoId),
                ticketDto.ResolvedOn,
                ticketDto.FirstMessage,
                ticketDto.TicketStatusId = Guid.Parse("0e06e836-471c-ec11-b063-14cb19ba19a9"),
                ticketDto.PriorityLevel = ticketDto.PriorityLevel,
                ticketDto.Remarks,
                ticketDto.ClosedOn,
                ticketDto.CancelledOn,
                ticketDto.TransferredOn,
                ticketDto.AssignedOn,
                ticketDto.ReopenedOn,
                ticketDto.PastDueDate,
                ticketDto.EnquiryId,
                ticketDto.ExpectedDueDate
            );
            await _unitOfWork.Repository<Ticket>().AddAsync(ticket);

            var ticketDetails = _mapper.Map<TicketDetailsDTO>(ticket);

            var recentActivityDto = new RecentActivityDTO();

            var userEmailAddress = await _applicationUserService.GetUserEmail(ticketDto.CustomerId.ToString());

            var enquiryVal = _enquiryCategoryService.GetEnquiryCategory(ticketDetails.EnquiryCategoryId);

            var saccoid = _applicationUserService.GetSaccoId(ticketDto.CustomerId.ToString());

            string saccoName = await _saccoTicketService.FindSaccoName(saccoid);

            await SendEmailNotification(ticketDetails, userEmailAddress);


            await _pusherService.SendPusherNotificationAsync(new
            {
                emailAddress = userEmailAddress,
                ticketNo = ticket.TicketNo,
                task = "New Ticket",
                color = "#385F73",
                icon = "mdi-new-box"
            }, "newTicket", "TicketCreated");

            recentActivityDto.ticketNo = ticket.TicketNo;
            recentActivityDto.saccoid = ticket.SaccoId;
            recentActivityDto.emailAddress = userEmailAddress;
            recentActivityDto.color = "#385F73";
            recentActivityDto.task = "New Ticket";
            recentActivityDto.icon = "mdi-new-box";

            await _recentActivityService.AddRecentActivityAsync(recentActivityDto);

            var link = $"https://agents.caprover.centrino.co.ke/ticket/{ticketDetails.TicketNo}";


            await SendSupportEmail(ticketDetails.TicketNo, userEmailAddress, enquiryVal, ticketDto.FirstMessage, link,ticketDto.AgentAddressed,saccoName);


            return ticketDetails;
        }

        public void SendReminderNotification(CancellationToken cancellationToken)
        {
            var reminderTikcets = _unitOfWork.Repository<TicketSummary>().FindAll(new TicketSummarySpecification())
                .ToList();

            for (int i = 0; i < reminderTikcets.Count; i++)
            {
                var formatOverdueby = TimeSpan.FromSeconds(Math.Abs(reminderTikcets[i].OverDueBy)).Days;
                
                _logger.LogInformation($"overdue by {Math.Abs(reminderTikcets[i].OverDueBy)}");
                _logger.LogInformation($"convert overdue value is {TimeSpan.FromSeconds(Math.Abs(reminderTikcets[i].OverDueBy))}");
                _logger.LogInformation($"final value is {TimeSpan.FromSeconds(Math.Abs(reminderTikcets[i].OverDueBy)).Days}");
                
                var reminderViewModel = new ReminderViewModel(reminderTikcets[i].TicketNo, formatOverdueby,
                    "https://agents.caprover.centrino.co.ke", reminderTikcets[i].FirstMessage,
                    reminderTikcets[i].RaisedBy, reminderTikcets[i].SaccoName);


                var mailviewDto = new MailViewModelDTO();
                mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
                mailviewDto.EmailTemplatePath = "/Views/Emails/ReminderEmail/ReminderTicketEmail.cshtml";
                mailviewDto.LinkedResourceContentId = "header";

                var builder = _reminderEmailTemplateResolver.BuildEmailBodyAsync(mailviewDto, reminderViewModel);


                _emailService.SendEmail(builder.Result.ToMessageBody(), reminderTikcets[i].Username, "Reminder Notification");
            }
        }

        public async Task<string> GetCareTakerAsync(string ticketNo)
        {
            var res = _unitOfWork.Repository<Ticket>()
                .FindAll(new TicketCareTakerSpecification(ticketNo)).First();


            return await _applicationUserService.GetUserEmail(res.CareTaker.ToString());
        }


        public async Task<bool> TransferTicket(string ticketNo, string agentId)
        {
            var persisted = _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketNo)).First();

            if (persisted != null)
            {
                var current = TicketFactory.CreateNewTicket(
                    persisted.CustomerId,
                    persisted.EnquiryCategoryId,
                    persisted.SaccoId,
                    persisted.CareTaker = Guid.Parse(agentId),
                    persisted.Attachments,
                    persisted.TicketNo,
                    persisted.ResolvedOn,
                    persisted.FirstMessage,
                    persisted.TicketStatusId = Guid.Parse("1ad2bda2-c923-ec11-8172-84a93e1f9479"),
                    persisted.PriorityLevel,
                    persisted.Remarks,
                    persisted.ClosedOn,
                    persisted.CancelledOn,
                    persisted.TransferredOn = DateTime.Now,
                    persisted.AssignedOn,
                    persisted.ReopenedOn,
                    persisted.PastDueDate,
                    persisted.EnquiryId,
                    persisted.ExpectedDueDate
                );

                _unitOfWork.Repository<Ticket>().Merge(persisted, current);

                var recentActivityDto = new RecentActivityDTO();

                var ticketDetails = _mapper.Map<TicketDetailsDTO>(current);

                var enquiryVal = _enquiryCategoryService.GetEnquiryCategory(persisted.EnquiryCategoryId);


                var oldagentEmailAddress = await _applicationUserService.GetUserEmail(persisted.CareTaker.ToString());

                var newAgentEmailAddress = await _applicationUserService.GetUserEmail(agentId);


                await SendTransferredEmailNotification(persisted.TicketNo, enquiryVal, oldagentEmailAddress,
                    newAgentEmailAddress);

                await _pusherService.SendPusherNotificationAsync(new
                {
                    emailAddress = newAgentEmailAddress,
                    ticketNo = current.TicketNo,
                    task = "Ticket Transferred",
                    color = "#00897B",
                    icon = "mdi-account-arrow-right-outline"
                }, "ticketTransferred", "TransferredTicket");

                recentActivityDto.ticketNo = current.TicketNo;
                recentActivityDto.saccoid = persisted.SaccoId;
                recentActivityDto.emailAddress = newAgentEmailAddress;
                recentActivityDto.color = "#385F73";
                recentActivityDto.task = "Ticket Transferred";
                recentActivityDto.icon = "mdi-new-box";

                await _recentActivityService.AddRecentActivityAsync(recentActivityDto);


                return true;
            }


            return false;
        }

        public async Task<bool> SendResolvedEmailNotification(TicketDetailsDTO ticketDetailsDto,
            string receiverEmailAddress)
        {
            var ticketNoViewModel = new ResolvedTicketViewModel(ticketDetailsDto.TicketNo, ticketDetailsDto.CreatedAt);
            var mailviewDto = new MailViewModelDTO();
            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            mailviewDto.EmailTemplatePath = "/Views/Emails/ResolvedTicket/ResolvedTicketEmail.cshtml";
            mailviewDto.LinkedResourceContentId = "header";

            var builder = await _resolvedTicketViewModel.BuildEmailBodyAsync(mailviewDto, ticketNoViewModel);


            await _emailService.SendEmailAsync(builder.ToMessageBody(), receiverEmailAddress, "Ticket Closed");

            return true;
        }

        public async Task<bool> SendTransferredEmailNotification(string ticketNo, string enquiry, string oldAgent,
            string receiverEmailAddress)
        {
            var transferredTicketViewModel = new TransferredTicketViewModel(ticketNo, enquiry, oldAgent);
            var mailviewDto = new MailViewModelDTO();
            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            mailviewDto.EmailTemplatePath = "/Views/Emails/TransferredTicket/TransferredTicketEmail.cshtml";
            mailviewDto.LinkedResourceContentId = "header";

            var builder =
                await _transferredTicketViewModel.BuildEmailBodyAsync(mailviewDto, transferredTicketViewModel);


            await _emailService.SendEmailAsync(builder.ToMessageBody(), receiverEmailAddress, "Ticket Transferred");

            return true;
        }


        public async Task<bool> SendEmailNotification(TicketDetailsDTO ticketDetailsDto, string receiverEmailAddress)
        {
            var ticketNoViewModel = new TicketNumberViewModel(ticketDetailsDto.TicketNo, ticketDetailsDto.CreatedAt);
            var mailviewDto = new MailViewModelDTO();
            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            mailviewDto.EmailTemplatePath = "/Views/Emails/TicketEmails/TicketNumberEmail.cshtml";
            mailviewDto.LinkedResourceContentId = "header";

            var builder = await _emailTemplateResolver.BuildEmailBodyAsync(mailviewDto, ticketNoViewModel);


            await _emailService.SendEmailAsync(builder.ToMessageBody(), receiverEmailAddress, "Ticket Created");

            return true;
        }

        public async Task<IEnumerable<Ticket>> FindTicketUserDetails(TicketDetailsDTO ticketDetailsDto)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketDetailsDto.TicketNo));
        }

        public int FindTransferredTickets()
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketTransferredSpecification()).Count();
        }

        public int FindOpenedTickets()
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketAssignedSpecification()).Count();
        }

        public int FindResolvedTickets()
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketsResolvedSpecification()).Count();
        }

        public int FindOverdueTickets()
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketPendingSpecification()).Count();
        }

        public int FindNewTickets()
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketNewSpecification()).Count();
        }

        public int FindClosedTickets()
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketClosedSpecification()).Count();
        }

        public int FindReopenedTickets()
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketReopenedSpecification()).Count();
        }

        public int FindSaccoTransferredTickets(Guid saccoId)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketTransferredSpecification()
                .And(new TicketSaccoSpecification(saccoId))).Count();
        }

        public int FindSaccoReopenedTickets(Guid saccoId)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketReopenedSpecification()
                .And(new TicketSaccoSpecification(saccoId))).Count();
        }

        public int FindSaccoOpenedTickets(Guid saccoId)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketAssignedSpecification()
                .And(new TicketSaccoSpecification(saccoId))).Count();
        }

        public int FindSaccoResolvedTickets(Guid saccoId)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketsResolvedSpecification()
                .And(new TicketSaccoSpecification(saccoId))).Count();
        }

        public int FindSaccoOverdueTickets(Guid saccoId)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketPendingSpecification()
                .And(new TicketSaccoSpecification(saccoId))).Count();
        }

        public int FindSaccoNewTickets(Guid saccoId)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketNewSpecification()
                .And(new TicketSaccoSpecification(saccoId))).Count();
        }

        public int FindSaccoClosedTickets(Guid saccoId)
        {
            return _unitOfWork.Repository<Ticket>().FindAll(new TicketClosedSpecification()
                .And(new TicketSaccoSpecification(saccoId))).Count();
        }

        public async Task<bool> ResolveTicket(string ticketNo)
        {
            var persisted = _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketNo)).First();

            if (persisted != null)
            {
                var current = TicketFactory.CreateNewTicket(
                    persisted.CustomerId,
                    persisted.EnquiryCategoryId,
                    persisted.SaccoId,
                    persisted.CareTaker,
                    persisted.Attachments,
                    persisted.TicketNo,
                    persisted.ResolvedOn = DateTime.Now,
                    persisted.FirstMessage,
                    persisted.TicketStatusId = Guid.Parse("02d5e20e-471c-ec11-b063-14cb19ba19a9"),
                    persisted.PriorityLevel,
                    persisted.Remarks,
                    persisted.ClosedOn,
                    persisted.CancelledOn,
                    persisted.TransferredOn,
                    persisted.AssignedOn,
                    persisted.ReopenedOn,
                    persisted.PastDueDate,
                    persisted.EnquiryId,
                    persisted.ExpectedDueDate
                );

                _unitOfWork.Repository<Ticket>().Merge(persisted, current);

                var recentActivityDto = new RecentActivityDTO();

                var ticketDetails = _mapper.Map<TicketDetailsDTO>(current);


                var userEmailAddress = await _applicationUserService.GetUserEmail(current.CustomerId.ToString());

                var agentEmailAddress = await _applicationUserService.GetUserEmail(current.CareTaker.ToString());


                await SendResolvedEmailNotification(ticketDetails, userEmailAddress);

                await _pusherService.SendPusherNotificationAsync(new
                {
                    emailAddress = agentEmailAddress,
                    ticketNo = current.TicketNo,
                    task = "Ticket Resolved",
                    color = "#3949AB",
                    icon = "mdi-checkbox-marked-circle-outline"
                }, "ticketResolved", "ResolvedTicket");

                recentActivityDto.ticketNo = current.TicketNo;
                recentActivityDto.saccoid = persisted.SaccoId;
                recentActivityDto.emailAddress = agentEmailAddress;
                recentActivityDto.color = "#3949AB";
                recentActivityDto.task = "Ticket Resolved";
                recentActivityDto.icon = "mdi-checkbox-marked-circle-outline";

                await _recentActivityService.AddRecentActivityAsync(recentActivityDto);


                return true;
            }


            return false;
        }

        public bool ReOpenTicket(string ticketNo, string remarks)
        {
            var persisted = _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketNo)).First();

            if (persisted != null)
            {
                var current = TicketFactory.CreateNewTicket(
                    persisted.CustomerId,
                    persisted.EnquiryCategoryId,
                    persisted.SaccoId,
                    persisted.CareTaker,
                    persisted.Attachments,
                    persisted.TicketNo,
                    persisted.ResolvedOn,
                    persisted.FirstMessage,
                    persisted.TicketStatusId = Guid.Parse("3e763c25-5677-4ea4-a754-9b3d9b09c463"),
                    persisted.PriorityLevel,
                    persisted.Remarks = remarks,
                    persisted.ClosedOn,
                    persisted.CancelledOn,
                    persisted.TransferredOn,
                    persisted.AssignedOn,
                    persisted.ReopenedOn = DateTime.Now,
                    persisted.PastDueDate,
                    persisted.EnquiryId,
                    persisted.ExpectedDueDate
                );

                _unitOfWork.Repository<Ticket>().Merge(persisted, current);
                return true;
            }

            return false;
        }

        public bool CloseTicket(string ticketNo)
        {
            var persisted = _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketNo)).First();

            if (persisted != null)
            {
                var current = TicketFactory.CreateNewTicket(
                    persisted.CustomerId,
                    persisted.EnquiryCategoryId,
                    persisted.SaccoId,
                    persisted.CareTaker,
                    persisted.Attachments,
                    persisted.TicketNo,
                    persisted.ResolvedOn,
                    persisted.FirstMessage,
                    persisted.TicketStatusId = Guid.Parse("35ba51b2-9987-4d5e-b3a2-460584a23deb"),
                    persisted.PriorityLevel,
                    persisted.Remarks,
                    persisted.ClosedOn = DateTime.Now,
                    persisted.CancelledOn,
                    persisted.TransferredOn,
                    persisted.AssignedOn,
                    persisted.ReopenedOn,
                    persisted.PastDueDate,
                    persisted.EnquiryId,
                    persisted.ExpectedDueDate
                );

                _unitOfWork.Repository<Ticket>().Merge(persisted, current);
                return true;
            }

            return false;
        }

        public bool MarkTicketAsPending(string ticketNo)
        {
            var persisted = _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketNo))
                .FirstOrDefault();

            if (persisted != null)
            {
                var current = TicketFactory.CreateNewTicket(
                    persisted.CustomerId,
                    persisted.EnquiryCategoryId,
                    persisted.SaccoId,
                    persisted.CareTaker,
                    persisted.Attachments,
                    persisted.TicketNo,
                    persisted.ResolvedOn,
                    persisted.FirstMessage,
                    persisted.TicketStatusId = Guid.Parse("e70fc103-471c-ec11-b063-14cb19ba19a9"),
                    persisted.PriorityLevel,
                    persisted.Remarks,
                    persisted.ClosedOn,
                    persisted.CancelledOn,
                    persisted.TransferredOn,
                    persisted.AssignedOn,
                    persisted.ReopenedOn,
                    persisted.PastDueDate = DateTime.Now,
                    persisted.EnquiryId,
                    persisted.ExpectedDueDate
                );

                _unitOfWork.Repository<Ticket>().Merge(persisted, current);
                return true;
            }

            return false;
        }

        public bool AssignAgentToUser(string ticketNo, Guid agentId)
        {
            var persisted = _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketNo)).First();

            if (persisted != null)
            {
                var current = TicketFactory.CreateNewTicket(
                    persisted.CustomerId,
                    persisted.EnquiryCategoryId,
                    persisted.SaccoId,
                    persisted.CareTaker = agentId,
                    persisted.Attachments,
                    persisted.TicketNo,
                    persisted.ResolvedOn,
                    persisted.FirstMessage,
                    persisted.TicketStatusId = Guid.Parse("2f049c19-471c-ec11-b063-14cb19ba19a9"),
                    persisted.PriorityLevel,
                    persisted.Remarks,
                    persisted.ClosedOn,
                    persisted.CancelledOn,
                    persisted.TransferredOn,
                    persisted.AssignedOn = DateTime.Now,
                    persisted.ReopenedOn,
                    persisted.PastDueDate,
                    persisted.EnquiryId,
                    persisted.ExpectedDueDate
                );

                _unitOfWork.Repository<Ticket>().Merge(persisted, current);
                return true;
            }

            return false;
        }

        public async Task<bool> ResolveSupportTicket(string ticketNo)
        {
            var persisted = _unitOfWork.Repository<Ticket>().FindAll(new TicketUserSpecification(ticketNo)).First();

            if (persisted != null)
            {
                var current = TicketFactory.CreateNewTicket(
                    persisted.CustomerId,
                    persisted.EnquiryCategoryId,
                    persisted.SaccoId,
                    persisted.CareTaker,
                    persisted.Attachments,
                    persisted.TicketNo,
                    persisted.ResolvedOn = DateTime.Now,
                    persisted.FirstMessage,
                    persisted.TicketStatusId = Guid.Parse("02d5e20e-471c-ec11-b063-14cb19ba19a9"),
                    persisted.PriorityLevel,
                    persisted.Remarks,
                    persisted.ClosedOn,
                    persisted.CancelledOn,
                    persisted.TransferredOn,
                    persisted.AssignedOn,
                    persisted.ReopenedOn,
                    persisted.PastDueDate,
                    persisted.EnquiryId,
                    persisted.ExpectedDueDate
                );

                _unitOfWork.Repository<Ticket>().Merge(persisted, current);
                return true;
            }

            return false;
        }

        public async Task<string> GetFirstMessage(string ticketNo)
        {
            var t = _unitOfWork.Repository<Ticket>().FindAll(new TicketMessageSpecification(ticketNo)).First();


            return t.FirstMessage;
        }

        public async Task<List<string>> GetAllTicketsAssignedToAgent(Guid agentId)
        {
            var ticketNumbers = new List<string>();
            var res = _unitOfWork.Repository<Ticket>().FindAll(new TicketAgentSpecification(agentId)).ToList();

            foreach (var obj in res) ticketNumbers.Add(obj.TicketNo);

            return ticketNumbers;
        }

        public async Task<List<string>> GetAllTicketsOpenedByUser(Guid userId)
        {
            var ticketNumbers = new List<string>();

            var res = _unitOfWork.Repository<Ticket>().FindAll(new TicketsOpenedByUserSpecification(userId)).ToList();

            foreach (var obj in res) ticketNumbers.Add(obj.TicketNo);

            return ticketNumbers;
        }

        public async Task<List<TicketInfoDTO>> GetAllTicketInformationByCustomerId(Guid customerId)
        {
            var res = _unitOfWork.Repository<TicketInformation>()
                .FindAll(new TicketByTicketInformationSpecification(customerId));

            return _mapper.Map<List<TicketInfoDTO>>(res);
        }

        public async Task<List<TicketInfoDTO>> GetResolvedTicketInformation()
        {
            var res = _unitOfWork.Repository<TicketInformation>()
                .FindAll(new TicketByClosedTicketInformationSpecification());

            return _mapper.Map<List<TicketInfoDTO>>(res);
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
                        && DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Hours >= 2) ///should be 2 hours
                    {
                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).FirstOrDefault();

                        if (inbox == null)
                        {
                            throw new ArgumentNullException(nameof(inbox),
                                $"{assignedTickets[i].TicketNo}missing inbox");
                        }


                        var url =
                            $"https://agents.caprover.centrino.co.ke/chat/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        MarkTicketAsPending(assignedTickets[i].TicketNo);

                        SendOverdueTicketEmail(assignedTickets[i].TicketNo, assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);
                    }

                    //ATM BRIDGE
                    if (assignedTickets[i].EnquiryId == Guid.Parse("cdc702bd-461c-ec11-b063-14cb19ba19a9")
                        && DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Hours >= 2) ///should be 2 hours
                    {
                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).FirstOrDefault();

                        var url =
                            $"https://agents.caprover.centrino.co.ke/chat/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        MarkTicketAsPending(assignedTickets[i].TicketNo);

                        SendOverdueTicketEmail(assignedTickets[i].TicketNo, assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);
                    }


                    //BULK SMS
                    if (assignedTickets[i].EnquiryId == Guid.Parse("7811e72d-4168-4652-9263-a6ec1d4974bf")
                        && DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Hours >= 24) //should be 24 hours
                    {
                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).First();

                        var url =
                            $"https://agents.caprover.centrino.co.ke/chat/{assignedTickets[i].TicketNo}/{inbox.Id}";


                        MarkTicketAsPending(assignedTickets[i].TicketNo);

                        SendOverdueTicketEmail(assignedTickets[i].TicketNo, assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);
                    }

                    //VANGUARD FINANCIALS
                    if (assignedTickets[i].EnquiryId == Guid.Parse("bdd797b0-461c-ec11-b063-14cb19ba19a9")
                        && DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Hours >= 24) //should be 24 hors
                    {
                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).First();

                        var url =
                            $"https://agents.caprover.centrino.co.ke/chat/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        MarkTicketAsPending(assignedTickets[i].TicketNo);

                        SendOverdueTicketEmail(assignedTickets[i].TicketNo, assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);
                    }

                    //MEMBERS PORTAL
                    if (assignedTickets[i].EnquiryId == Guid.Parse("b01e2bad-e787-4777-a2f6-8219e609c503")
                        && DateTime.Now.Subtract(assignedTickets[i].CreatedAt).Hours >= 24)
                    {
                        var rUserEmail = _applicationUserService.GetEmail(assignedTickets[i].CareTaker.ToString());

                        var inbox = _unitOfWork.Repository<Inbox>()
                            .FindAll(new InboxTicketSpecification(assignedTickets[i].TicketNo)).First();

                        var url =
                            $"https://agents.caprover.centrino.co.ke/chat/{assignedTickets[i].TicketNo}/{inbox.Id}";

                        MarkTicketAsPending(assignedTickets[i].TicketNo);

                        SendOverdueTicketEmail(assignedTickets[i].TicketNo, assignedTickets[i].FirstMessage, url,
                            rUserEmail, rUserEmail);
                    }
                }
            }
        }

        public void SendOverdueTicketEmail(string ticketNo, string firstMessage, string url, string careTaker,
            string receiverEmailAddress)
        {
            var overdueTicketModel = new OverdueTicketModel(ticketNo, firstMessage, url, careTaker);
            var mailviewDto = new MailViewModelDTO();
            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            mailviewDto.EmailTemplatePath = "/Views/Emails/OverdueTicket/OverdueTicket.cshtml";
            mailviewDto.LinkedResourceContentId = "header";

            var builder = _overdueTicketEmailViewModel.BuildEmailBodyAsync(mailviewDto, overdueTicketModel);


            _emailService.SendEmailAsync(builder.Result.ToMessageBody(), receiverEmailAddress, "Overdue Ticket");
        }


        public async Task<List<TicketInfoDTO>> GetClosedTicketInformation()
        {
            var res = _unitOfWork.Repository<TicketInformation>()
                .FindAll(new TicketInformationClosedSpecification());

            return _mapper.Map<List<TicketInfoDTO>>(res);
        }


        public async Task<bool> SendSupportEmail(string ticketNumber, string clientEmailAddress, string enquiry,
            string issue, string url, List<string> agentsAddressed,string subject)
        {
            var supportViewModel = new SupportEmailViewModel(ticketNumber, clientEmailAddress, enquiry, issue, url, agentsAddressed);

            var mailviewDto = new MailViewModelDTO();

            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";

            mailviewDto.EmailTemplatePath = "/Views/Emails/SupportEmail/SupportEmail.cshtml";

            mailviewDto.LinkedResourceContentId = "header";

            var builder = await _supportEmailViewModel.BuildEmailBodyAsync(mailviewDto, supportViewModel);


            await _emailService.SendEmailAsync(builder.ToMessageBody(),
                _configuration.GetSection("ServerSettings").GetSection("EmailAddress").Value,
                $"New Ticket-{subject}");

            return true;
        }
    }
}