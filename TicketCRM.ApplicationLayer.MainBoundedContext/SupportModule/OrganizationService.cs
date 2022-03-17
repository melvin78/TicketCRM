using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.SaccoSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class OrganizationService:IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddNewOrganisationAsync(OrganizationDTO organizationDto)
        {
            if (organizationDto == null) return 0;

            var organization = OrganizationFactory.CreateNewOrganization(organizationDto.OrganizationName);

            return await _unitOfWork.Repository<Organization>().AddAsync(organization);

        }

        public async Task<List<Organization>> GetOrganisationAsync()
        {
            return await _unitOfWork.Repository<Organization>().GetAllAsync();
        }

        public  string FindOrganisationName(Guid? organizationId)
        {
            return _unitOfWork.Repository<Organization>().FindAll(new SaccoBaseSpecification(organizationId)).First().OrganizationName;
        }
    }
}