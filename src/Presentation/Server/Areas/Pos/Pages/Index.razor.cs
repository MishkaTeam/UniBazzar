namespace Server.Areas.Pos.Pages;

public partial class Index
{
    public Task onProductSelection(Guid productId)
    {
        Console.WriteLine(productId);
        return Task.CompletedTask;
    }
}