using AutoMapper;
using Newtonsoft.Json;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using AgentDTO = IdentityServerAspNetIdentity.Configuration.AgentDTO;

namespace TicketCRM
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
          
            
             //->Support mappings
            CreateMap<Enquiries, EnquiriesDTO>();
            CreateMap<EnquiryCategory, EnquiryCategoryDTO>();
            CreateMap<Ticket, OverdueTicketDTO>();
            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.AgentAddressed, opt => opt.Ignore());
            CreateMap<Ticket, TicketDetailsDTO>();
            CreateMap<TicketReport, TicketReportClientDTO>();
            CreateMap<TicketReport, TicketReportAgentDTO>();
            CreateMap<TicketSummary, TicketReportSummaryDTO>();
            CreateMap<Priority, PriorityLevelDTO>();
            CreateMap<RecentActivity, RecentActivityDTO>()
                .ForMember(dest => dest.color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.icon, opt => opt.MapFrom(src => src.Icon))
                .ForMember(dest => dest.show, opt => opt.Ignore())
                .ForMember(dest => dest.task, opt => opt.MapFrom(src => src.Task))
                .ForMember(dest => dest.emailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.ticketNo, opt => opt.MapFrom(src => src.TicketNumber));
            CreateMap<DateTime, string>().ConstructUsing(dt => String.Format("{0:G}", dt.AddHours(3)));
            CreateMap<Agent, AgentDTO>();
            CreateMap<Agent, AgentDetailsDTO>();
                // .ForMember(dest => dest.Name, dest => dest
                //     .MapFrom(x => $"{x.FirstName}{x.SecondName}"));
            CreateMap<TicketAssignment, TicketAssignmentDTO>();
            CreateMap<TicketInformation, TicketInfoDTO>()
                .ForMember(dest => dest.ticketnumber, opt => opt.MapFrom(src => src.TicketNo))
                .ForMember(dest => dest.openedon, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.enquirycategory, opt => opt.MapFrom(src => src.EnquiryCategoryVal))
                .ForMember(dest => dest.ticketstatus, opt => opt.MapFrom(src => src.TicketStatusVal))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.attachments,
                    opt => opt.MapFrom(src => JsonConvert.DeserializeObject(src.Attachments)))
                .ForMember(dest => dest.firstmessage, opt => opt.MapFrom(src => src.FirstMessage))
                .ForMember(dest => dest.resolvedon, opt => opt.MapFrom(src => src.ResolvedOn))
                .ForMember(dest => dest.caretaker, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.dateassigned, opt => opt.MapFrom(src => src.DateAssigned))
                .ForMember(dest => dest.remarks, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(dest => dest.closedon, opt => opt.MapFrom((src => src.ClosedOn)))
                .ForMember(dest => dest.reopeneddate, opt => opt.MapFrom(src => src.ReopenedOn))
                .ForMember(dest => dest.organizationid, opt => opt.MapFrom(src => src.OrganizationId));



            CreateMap<Inbox, InboxDTO>();
            CreateMap<Inbox, SingleRoomDTO>();
              


            CreateMap<Chats, FormattedChatDTO>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(o => o.ChatId))
                .ForMember(dest => dest.avatar, opt => opt.MapFrom(o => o.Avatar))
                .ForMember(dest => dest.content, opt => opt.MapFrom(o => o.Content))
                .ForMember(dest => dest.files , opt => opt.MapFrom(o => o.ChatFile.Url))
                .ForMember(dest => dest.timestamp, opt => opt.MapFrom(o => o.TimeStamp))
                .ForMember(dest => dest.username, opt => opt.MapFrom(o => o.UserName))
                .ForMember(dest => dest.senderId, opt => opt.MapFrom(o => o.SenderId));

            CreateMap<Chats, ChatDTO>()
                .ForMember(dest => dest.SocketId, opt => opt.Ignore())
                .ForMember(dest => dest.ReceiverEmailAddress, opt => opt.Ignore());

            
            CreateMap<Response, ResponseDTO>()
                .ForMember(dest=>dest.SocketId,opt=>opt.Ignore())
                .ForMember(o => o.CreatedAt
                    , opt => opt.MapFrom(src => src.CreatedAt.ToString("f")));


        }
    }
}