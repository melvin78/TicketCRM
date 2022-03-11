using System.Threading.Tasks;

namespace TicketCRM.DataLayer.EmailTemplates.Services
{
    public interface IRazorViewToStringRenderer
    {
        public Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
        
        public string RenderViewToString<TModel>(string viewName, TModel model);

    }
}