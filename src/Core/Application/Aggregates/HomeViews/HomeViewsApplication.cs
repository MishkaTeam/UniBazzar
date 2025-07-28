using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;
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
            (await homeViewRepository
            .GetAllAsync())
            .OrderBy(x => x.Ordering)
            .ToList();

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
        var homeViewForDelete =
            await homeViewRepository.GetByIdAsync(homeViewId);

        if (homeViewForDelete == null || homeViewForDelete.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        await homeViewRepository.RemoveAsync(homeViewForDelete);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ResultContract<SliderViewItemViewModel>> AddSliderItem(CreateSliderViewItemViewModel viewModel)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(viewModel.HomeViewId);

        if (homeView == null || homeView.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var sliderItem =
            SlideViewItem.Create(
                viewModel.HomeViewId,
                viewModel.Title,
                viewModel.ImageUrl,
                viewModel.NavigationUrl,
                viewModel.Interval,
                viewModel.Ordering);

        var result =
            homeView.AddSlide(sliderItem);

        if (result == false)
        {
            return (ErrorType.InvalidFormat, "Error type.");
        }

        await unitOfWork.SaveChangesAsync();

        return sliderItem.Adapt<SliderViewItemViewModel>();
    }

    public async Task<ResultContract<List<SliderViewItemViewModel>>> GetSliderItems(Guid homeViewId)
    {
        var sliderItems =
            (await homeViewRepository
            .GetSliderItemsByIdAsync(homeViewId))
            .OrderBy(x => x.Ordering)
            .ToList();

        return sliderItems.Adapt<List<SliderViewItemViewModel>>();
    }

    public async Task<ResultContract<SliderViewItemViewModel>> GetSliderItem(Guid homeViewId, Guid sliderItemId)
    {
        var sliderItems =
            (await GetSliderItems(homeViewId)).Data;

        if (sliderItems == null)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var sliderItem =
            sliderItems.FirstOrDefault(x => x.Id == sliderItemId);

        return sliderItem.Adapt<SliderViewItemViewModel>();
    }

    public async Task<ResultContract<SliderViewItemViewModel>> UpdateSliderItem(UpdateSliderViewItemViewModel viewModel)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(viewModel.HomeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Slider)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var sliderItemForUpdate =
            homeView.SliderViews
            .FirstOrDefault(x => x.Id != viewModel.Id);

        if (sliderItemForUpdate == null ||
            sliderItemForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.SliderView);

            return (ErrorType.NotFound, message);
        }

        sliderItemForUpdate.Update(
            viewModel.Title,
            viewModel.NavigationUrl,
            viewModel.Interval,
            viewModel.Ordering);

        await unitOfWork.SaveChangesAsync();
        return sliderItemForUpdate.Adapt<SliderViewItemViewModel>();
    }

    public async Task<ResultContract> DeleteSliderItem(Guid homeViewId, Guid sliderItemId)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(homeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Slider)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var sliderItemForDelete =
            homeView.SliderViews
            .FirstOrDefault(x => x.Id != sliderItemId);

        if (sliderItemForDelete == null ||
            sliderItemForDelete.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.SliderView);

            return (ErrorType.NotFound, message);
        }

        homeView.SliderViews.Remove(sliderItemForDelete);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
