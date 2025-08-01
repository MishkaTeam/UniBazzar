using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ImageViewItems;
using Application.Aggregates.HomeViews.ViewModels.ProductViewItems;
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


    #region [ Home View ] Methods

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

    #endregion


    #region [ Slider Item ] Methods

    public async Task<ResultContract<SliderViewItemViewModel>> AddSliderItem(CreateSliderViewItemViewModel viewModel)
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
            .FirstOrDefault(x => x.Id == viewModel.Id);

        if (sliderItemForUpdate == null ||
            sliderItemForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.SliderView);

            return (ErrorType.NotFound, message);
        }

        sliderItemForUpdate.Update(
            viewModel.Title,
            viewModel.ImageUrl,
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
            .FirstOrDefault(x => x.Id == sliderItemId);

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

    #endregion


    #region [ Product Item ] Methods

    public async Task<ResultContract<ProductViewItemViewModel>> AddProductItem(CreateProductViewItemViewModel viewModel)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(viewModel.HomeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Product)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ProductView);

            return (ErrorType.NotFound, message);
        }

        var productItem =
            ProductViewItem.Create(
                viewModel.HomeViewId,
                viewModel.ProductId,
                viewModel.Ordering);

        var result =
            homeView.AddProduct(productItem);

        if (result == false)
        {
            return (ErrorType.InvalidFormat, "Error type.");
        }

        await unitOfWork.SaveChangesAsync();
        return ProductViewItemViewModel.FromProductViewItem(productItem);
    }

    public async Task<ResultContract<List<ProductViewItemViewModel>>> GetProductItems(Guid homeViewId)
    {
        var productItems =
            (await homeViewRepository
            .GetProductItemsByIdAsync(homeViewId))
            .OrderBy(x => x.Ordering)
            .ToList();

        return ProductViewItemViewModel.FromProductViewItemList(productItems);
    }

    public async Task<ResultContract<ProductViewItemViewModel>> GetProductItem(Guid homeViewId, Guid productItemId)
    {
        var productItems =
            (await GetProductItems(homeViewId)).Data;

        if (productItems == null)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var productItem =
            productItems.FirstOrDefault(x => x.Id == productItemId);

        return productItem!;
    }

    public async Task<ResultContract<ProductViewItemViewModel>> UpdateProductItem(UpdateProductViewItemViewModel viewModel)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(viewModel.HomeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Product)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var productItemForUpdate =
            homeView.ProductViews
            .FirstOrDefault(x => x.Id == viewModel.Id);

        if (productItemForUpdate == null ||
            productItemForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ProductView);

            return (ErrorType.NotFound, message);
        }

        productItemForUpdate.Update(
            viewModel.ProductId,
            viewModel.Ordering);

        await unitOfWork.SaveChangesAsync();
        return ProductViewItemViewModel.FromProductViewItem(productItemForUpdate);
    }

    public async Task<ResultContract> DeleteProductItem(Guid homeViewId, Guid productItemId)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(homeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Product)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var productItemForDelete =
            homeView.ProductViews
            .FirstOrDefault(x => x.Id == productItemId);

        if (productItemForDelete == null ||
            productItemForDelete.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ProductView);

            return (ErrorType.NotFound, message);
        }

        homeView.ProductViews.Remove(productItemForDelete);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    #endregion


    #region [ Image Item ] Methods

    public async Task<ResultContract<ImageViewItemViewModel>> AddImageItem(CreateImageViewItemViewModel viewModel)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(viewModel.HomeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Image)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var imageItem =
            ImageViewItem.Create(
                viewModel.HomeViewId,
                viewModel.Title,
                viewModel.ImageUrl,
                viewModel.NavigationUrl,
                viewModel.Column,
                viewModel.Ordering);

        var result =
            homeView.AddImage(imageItem);

        if (result == false)
        {
            return (ErrorType.InvalidFormat, "Error type.");
        }

        await unitOfWork.SaveChangesAsync();
        return imageItem.Adapt<ImageViewItemViewModel>();
    }

    public async Task<ResultContract<List<ImageViewItemViewModel>>> GetImageItems(Guid homeViewId)
    {
        var mageItems =
            (await homeViewRepository
            .GetImageItemsByIdAsync(homeViewId))
            .OrderBy(x => x.Ordering)
            .ToList();

        return mageItems.Adapt<List<ImageViewItemViewModel>>();
    }

    public async Task<ResultContract<ImageViewItemViewModel>> GetImageItem(Guid homeViewId, Guid imageItemId)
    {
        var imageItems =
            (await GetImageItems(homeViewId)).Data;

        if (imageItems == null)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var imageItem =
            imageItems.FirstOrDefault(x => x.Id == imageItemId);

        return imageItem.Adapt<ImageViewItemViewModel>();
    }

    public async Task<ResultContract<ImageViewItemViewModel>> UpdateImageItem(UpdateImageViewItemViewModel viewModel)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(viewModel.HomeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Image)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var imageItemForUpdate =
            homeView.ImageViews
            .FirstOrDefault(x => x.Id == viewModel.Id);

        if (imageItemForUpdate == null ||
            imageItemForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ImageView);

            return (ErrorType.NotFound, message);
        }

        imageItemForUpdate.Update(
            viewModel.Title,
            viewModel.ImageUrl,
            viewModel.NavigationUrl,
            viewModel.Column,
            viewModel.Ordering);

        await unitOfWork.SaveChangesAsync();
        return imageItemForUpdate.Adapt<ImageViewItemViewModel>();
    }

    public async Task<ResultContract> DeleteImageItem(Guid homeViewId, Guid imageItemId)
    {
        var homeView =
            await homeViewRepository.GetByIdAsync(homeViewId);

        if (homeView == null ||
            homeView.Id == Guid.Empty ||
            homeView.Type != ViewType.Image)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.HomeView);

            return (ErrorType.NotFound, message);
        }

        var imageItemForDelete =
            homeView.ImageViews
            .FirstOrDefault(x => x.Id == imageItemId);

        if (imageItemForDelete == null ||
            imageItemForDelete.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ImageView);

            return (ErrorType.NotFound, message);
        }

        homeView.ImageViews.Remove(imageItemForDelete);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    #endregion

}
