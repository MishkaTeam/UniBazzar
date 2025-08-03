using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.Extensions;
using Domain.Aggregates.Cms.HomeViews;
using Domain.Aggregates.Cms.HomeViews.Data;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.HomeViews;

public class HomeViewRepository
    (UniBazzarContext uniBazzarContext,
    IExecutionContextAccessor executionContext)
    : RepositoryBase<HomeView>(uniBazzarContext, executionContext), IHomeViewRepository
{
    public async Task<List<HomeView>> GetAllWithIncludeAsync()
    {
        var sliderViews = uniBazzarContext.HomeViews
                    .StoreFilter(ExecutionContext.StoreId)
                    .Where(x => x.Type == ViewType.Slider)
                    .Include(x => x.SliderViews)
                    .AsNoTracking();

        var productViews = uniBazzarContext.HomeViews
                    .StoreFilter(ExecutionContext.StoreId)
                    .Where(x => x.Type == ViewType.Product)
                    .Include(x => x.ProductViews)
                    .AsNoTracking();

        var imageViews = uniBazzarContext.HomeViews
                    .StoreFilter(ExecutionContext.StoreId)
                    .Where(x => x.Type == ViewType.Image)
                    .Include(x => x.ImageViews)
                    .AsNoTracking();

        var result = await sliderViews
                    .Union(productViews)
                    .Union(imageViews)
                    .ToListAsync();

        return result;
    }

    public async Task<List<SlideViewItem>> GetSliderItemsByIdAsync(Guid homeViewId)
    {
        var homeView = await uniBazzarContext.HomeViews
                    .StoreFilter(ExecutionContext.StoreId)
                    .Include(x => x.SliderViews)
                    .Where(x => x.Type == ViewType.Slider)
                    .FirstOrDefaultAsync(x => x.Id == homeViewId);

        if (homeView == null)
        {
            return null!;
        }

        return homeView.SliderViews;
    }

    public async Task<List<ImageViewItem>> GetImageItemsByIdAsync(Guid homeViewId)
    {
        var homeView = await uniBazzarContext.HomeViews
                    .StoreFilter(ExecutionContext.StoreId)
                    .Include(x => x.ImageViews)
                    .Where(x => x.Type == ViewType.Image)
                    .FirstOrDefaultAsync(x => x.Id == homeViewId);

        if (homeView == null)
        {
            return null!;
        }

        return homeView.ImageViews;
    }

    public async Task<List<ProductViewItem>> GetProductItemsByIdAsync(Guid homeViewId)
    {
        var homeView = await uniBazzarContext.HomeViews
                    .StoreFilter(ExecutionContext.StoreId)
                    .Include(x => x.ProductViews)
                    .Where(x => x.Type == ViewType.Product)
                    .FirstOrDefaultAsync(x => x.Id == homeViewId);

        if (homeView == null)
        {
            return null!;
        }

        return homeView.ProductViews;
    }
}
