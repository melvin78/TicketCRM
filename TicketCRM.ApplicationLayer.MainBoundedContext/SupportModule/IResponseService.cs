using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IResponseService
    {

        Task<bool> AddResponseAsync(ResponseDTO responseDto);

        Task<List<ResponseDTO>> GetAllResponsesAsync(string ticketNo);

        Task<bool> MarkAsRead(Guid responseId);
        
        List<RoomDTO> ConfigureRooms(Guid userId);

        Task<List<ResponseDTO>> UnreadResponses(Guid toId);
        
        



    }
}