using AutoMapper;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquirySpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class EnquiriesService:IEnquiriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnquiriesService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddNewEnquiryAsync(EnquiriesDTO enquiriesDto)
        {
            if ( enquiriesDto == null) return 0;
            

            var enquiry = EnquiriesFactory.CreateNewEnquiry(enquiriesDto.EnquiryType);
            
            return await _unitOfWork.Repository<Enquiries>().AddAsync(enquiry);
        }

        public async Task<List<Enquiries>> GetEnquiriesAsync()
        {
            
            return await _unitOfWork.Repository<Enquiries>().GetAllAsync();
        }

        public Guid GetParentEntityId(string enquiryType)
        {
            var res = Guid.Empty;
            
            var enquiries =_unitOfWork.Repository<Enquiries>().FindAll(new EnquiryBaseSpecification(enquiryType));

            foreach (var obj in enquiries)
            {
                res = obj.Id;
            }
            
            return res;


        }
    }
}