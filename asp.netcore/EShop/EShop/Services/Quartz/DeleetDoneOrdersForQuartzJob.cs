using Quartz;

namespace EShop.Services.Quartz;

public class DeleetDoneOrdersForQuartzJob : IJob
{
    private readonly IDeleteDoneOrdersMyServiceInterface deleteDoneOrders;

    public DeleetDoneOrdersForQuartzJob(IDeleteDoneOrdersMyServiceInterface deleteDoneOrders)
    {
        this.deleteDoneOrders = deleteDoneOrders;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        await deleteDoneOrders.Delete();
    }
}
