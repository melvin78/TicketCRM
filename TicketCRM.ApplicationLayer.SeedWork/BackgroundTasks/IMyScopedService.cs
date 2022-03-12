namespace TicketCRM.ApplicationLayer.SeedWork.BackgroundTasks
{
    public interface IMyScopedService<T>
    {
        Task DoWork(CancellationToken cancellationToken);

    }
}