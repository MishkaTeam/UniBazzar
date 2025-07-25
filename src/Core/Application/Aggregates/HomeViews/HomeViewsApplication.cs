using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Domain;
using Domain.Aggregates.Cms.HomeViews;
using Domain.Aggregates.Cms.HomeViews.Data;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Framework.DataType;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.HomeViews;

public class HomeViewsApplication
    (IHomeViewRepository homeViewRepository, IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<List<HomeViewViewModel>>> CreateSystemicViews(Guid storeId)
    {
        var views =
            new List<HomeView>();

        views.AddRange(
            HomeView.Create("Slider", ViewType.Slider, 1),
            HomeView.Create("Featured Products", ViewType.Product, 2),
            HomeView.Create("Discounted Products", ViewType.Product, 3),
            HomeView.Create("Images", ViewType.Image, 4));

        await homeViewRepository.AddRangeAsync(views);

        foreach (var view in views)
        {
            view.SetStore(storeId);
            view.MakeSystemic();
        }

        await unitOfWork.SaveChangesAsync();

        return views.Adapt<List<HomeViewViewModel>>();
    }

    public async Task<ResultContract<HomeViewViewModel>> AddHomeView(CreateHomeViewViewModel viewModel)
    {
        var homeView =
            HomeView.Create(
            viewModel.Title,
            viewModel.Type,
            viewModel.Ordering);

        await homeViewRepository.AddAsync(homeView);
        await unitOfWork.SaveChangesAsync();

        return homeView.Adapt<HomeViewViewModel>();
    }

    public async Task<ResultContract<List<HomeViewViewModel>>> GetHomeViews()
    {
        var homeViews =
            (await homeViewRepository.GetAllAsync())
            .OrderBy(x => x.Ordering);

        return homeViews.Adapt<List<HomeViewViewModel>>();
    }

    public async Task<ResultContract<HomeViewViewModel>> GetHomeViewAsync(Guid id)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(id);

        if (homeView == null || homeView.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        return homeView.Adapt<HomeViewViewModel>();
    }

    public async Task<ResultContract<HomeViewViewModel>> UpdateHomeView(UpdateHomeViewViewModel viewModel)
    {
        var homeViewForUpdate =
            await homeViewRepository.GetByIdAsync(viewModel.Id);

        if (homeViewForUpdate == null || homeViewForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        homeViewForUpdate.Update
            (viewModel.Title, viewModel.Ordering);

        await unitOfWork.SaveChangesAsync();
        return homeViewForUpdate.Adapt<HomeViewViewModel>();
    }

    public async Task<ResultContract> DeleteHomeView(Guid homeViewId)
    {
        var homeViewForUpdate =
            await homeViewRepository.GetByIdAsync(homeViewId);

        if (homeViewForUpdate == null || homeViewForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        await homeViewRepository.RemoveAsync(homeViewForUpdate);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
