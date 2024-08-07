using EShop.Models;

namespace EShop.Services.Quartz;

public class MyServiceDeleteDoneOrders : IDeleteDoneOrdersMyServiceInterface
{
    private readonly AplicationDbContext context;
    private readonly BrevoEmailService emailService;

    public MyServiceDeleteDoneOrders(AplicationDbContext context, BrevoEmailService emailService)
    {
        this.context = context;
        this.emailService = emailService;
    }

    public async Task Delete()
    {
        await Console.Out.WriteLineAsync("backround service is started!");
        var doneOrders = context.Orders.Where(x => x.OrderStatus == "Done");
        context.Orders.RemoveRange(doneOrders);
        await context.SaveChangesAsync();
        await emailService.SendEmailAsync("kamran.eilink@gmail.com", "order's are removed", "some order's is removed!!!");
    }
}
