using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Cms.HomeViews.Data;

public interface IHomeViewRepository : IRepositoryBase<HomeView>
{
    Task<List<HomeView>> GetAllWithIncludeAsync();
    Task<List<SlideViewItem>> GetSliderItemsByIdAsync(Guid homeViewId);
    Task<List<ImageViewItem>> GetImageItemsByIdAsync(Guid homeViewId);
    Task<List<ProductViewItem>> GetProductItemsByIdAsync(Guid homeViewId);
}
