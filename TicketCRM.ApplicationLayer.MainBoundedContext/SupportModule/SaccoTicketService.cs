using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.Factories;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.SaccoSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public class SaccoTicketService:ISaccoTicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaccoTicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddNewSaccoAsync(SaccoDTO saccoDto)
        {
            if (saccoDto == null) return 0;

            var sacco = SaccoFactory.CreateNewSacco(saccoDto.SaccoName);

            return await _unitOfWork.Repository<Sacco>().AddAsync(sacco);

        }

        public async Task<List<Sacco>> GetSaccoAsync()
        {
            return await _unitOfWork.Repository<Sacco>().GetAllAsync();
        }

        public async Task<string> FindSaccoName(Guid? saccoid)
        {
            return _unitOfWork.Repository<Sacco>().FindAll(new SaccoBaseSpecification(saccoid)).First().SaccoName;
        }
    }
}