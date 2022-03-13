using AutoMapper;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.RecentActivitySpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class RecentActivityService:IRecentActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecentActivityService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public int AddRecentActivity(RecentActivityDTO recentActivityDto)
        {
            if (recentActivityDto == null) return 0;

            var recentActivity = RecentActivityFactory.
                AddRecentActivity(
                    recentActivityDto.color,
                    recentActivityDto.icon,
                    recentActivityDto.task,
                    recentActivityDto.ticketNo,
                    recentActivityDto.emailAddress,
                    recentActivityDto.saccoid
                );

            return  _unitOfWork.Repository<RecentActivity>().Add(recentActivity);
        }

        public async Task<int> AddRecentActivityAsync(RecentActivityDTO recentActivityDto)
        {
            if (recentActivityDto == null) return 0;

            var recentActivity = RecentActivityFactory.
                AddRecentActivity(
                    recentActivityDto.color,
                    recentActivityDto.icon,
                    recentActivityDto.task,
                    recentActivityDto.ticketNo,
                    recentActivityDto.emailAddress,
                    recentActivityDto.saccoid
                );

            return await  _unitOfWork.Repository<RecentActivity>().AddAsync(recentActivity);
        }

        public List<RecentActivityDTO> FindRecentActivitiesAsync()
        {
            var res=  _unitOfWork.Repository<RecentActivity>().FindAll(new RecentActivityBaseSpecification())
                .ToList();

            return _mapper.Map<List<RecentActivityDTO>>(res);
        }

        public List<RecentActivityDTO> FindRecentActivitiesAsync(string saccoId)
        {
            var res=  _unitOfWork.Repository<RecentActivity>().FindAll(new RecentActivityBaseSpecification()
                    .And(new RecentActivitySaccoSpecification(Guid.Parse(saccoId))))
                .ToList();

            return _mapper.Map<List<RecentActivityDTO>>(res);
        }
    }
}