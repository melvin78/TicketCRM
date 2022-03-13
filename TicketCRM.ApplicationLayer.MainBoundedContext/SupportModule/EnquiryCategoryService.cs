using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquiryCategorySpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class EnquiryCategoryService:IEnquiryCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnquiryCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddNewEnquiryCategoryAsync(EnquiryCategoryDTO enquiryCategoryDto)
        {
            if (enquiryCategoryDto == null) return 0;

            var enquiryCategory =
                EnquiryCategoryFactory.CreateNewEnquiryCategory(enquiryCategoryDto.EnquiryCategoryVal,
                    enquiryCategoryDto.EnquiryId);
            
            return await _unitOfWork.Repository<EnquiryCategory>().AddAsync(enquiryCategory);
        }

        public string GetEnquiryCategory(Guid enquiryCategory)
        {
            return _unitOfWork.Repository<EnquiryCategory>()
                .FindAll(new EnquiryCategorySpecificationId(enquiryCategory)).First().EnquiryCategoryVal;
        }

        public async Task<List<EnquiryCategory>> GetEnquiryCategoryAsync()
        {
            return await _unitOfWork.Repository<EnquiryCategory>().GetAllAsync();
        }

        public async Task<List<EnquiryCategory>> GetEnquiryCategoryById(Guid enquiryCategoryId)
        {
            return  _unitOfWork.Repository<EnquiryCategory>().FindAll(new EnquiryCategoryByIdSpecification(enquiryCategoryId)).ToList();
        }

        public async Task<List<EnquiryCategory>> GetEnquiryCategoryByEnquiry(string enquiryCategoryVal)
        {
            return _unitOfWork.Repository<EnquiryCategory>()
                .FindAll(new EnquiryCategoryByEnquirySpecification(enquiryCategoryVal)).ToList();
        }
    }
}