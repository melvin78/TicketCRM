using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface ISaccoTicketService
    {
        Task<int> AddNewSaccoAsync(SaccoDTO saccoDto);
        
        Task<List<Sacco>> GetSaccoAsync();


        Task<string> FindSaccoName(Guid? saccoid);
    }
}