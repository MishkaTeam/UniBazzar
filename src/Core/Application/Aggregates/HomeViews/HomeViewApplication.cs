using Domain;
using Domain.Aggregates.Cms.HomeViews;
using Domain.Aggregates.Cms.HomeViews.Data;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Framework.DataType;

namespace Application.Aggregates.HomeViews;

public class HomeViewApplication
    (IHomeViewRepository homeViewRepository, IUnitOfWork unitOfWork)
{
    public async Task<ResultContract> CreateSystemicViews(Guid storeId)
    {
        var views =
            new List<HomeView>();

        views.AddRange(
            HomeView.Create("Slider", ViewType.Slider, 1),
            HomeView.Create("Featured Products", ViewType.Product, 2),
            HomeView.Create("Discounted Products", ViewType.Product, 3),
            HomeView.Create("Iamges", ViewType.Image, 4));

        await homeViewRepository.AddRangeAsync(views);

        foreach (var view in views)
        {
            view.SetStore(storeId);
            view.MakeSystemic();
        }

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
