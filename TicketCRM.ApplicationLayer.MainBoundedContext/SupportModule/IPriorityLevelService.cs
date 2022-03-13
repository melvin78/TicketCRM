using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IPriorityLevelService
    {

        int AddPriorityLevel(PriorityLevelDTO priorityLevelDto);

        List<PriorityLevelDTO> FindPrioritiesLevel();

    }
}