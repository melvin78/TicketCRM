using AutoMapper;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class PriorityLevelService:IPriorityLevelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PriorityLevelService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public int AddPriorityLevel(PriorityLevelDTO priorityLevelDto)
        {
            if ( priorityLevelDto == null) return 0;
            

            var priorityLevel = PriorityFactory.AddNewPriority(priorityLevelDto.Description,priorityLevelDto.Level);
            
            return  _unitOfWork.Repository<Priority>().Add(priorityLevel);
        }

        public List<PriorityLevelDTO> FindPrioritiesLevel()
        {
            var res = _unitOfWork.Repository<Priority>().GetAll();

            return _mapper.Map<List<PriorityLevelDTO>>(res);
        }
    }
}