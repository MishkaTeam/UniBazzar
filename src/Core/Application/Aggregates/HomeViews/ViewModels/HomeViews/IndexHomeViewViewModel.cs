using Application.Aggregates.HomeViews.ViewModels.ImageViewItems;
using Application.Aggregates.HomeViews.ViewModels.ProductViewItems;
using Application.Aggregates.HomeViews.ViewModels.SliderViewItems;

namespace Application.Aggregates.HomeViews.ViewModels.HomeViews;

public class IndexHomeViewViewModel : HomeViewViewModel
{
    public IndexHomeViewViewModel()
    {
    }


    public List<SliderViewItemViewModel> SliderViews { get; set; }
    public List<ProductViewItemViewModel> ProductViews { get; set; }
    public List<ImageViewItemViewModel> ImageViews { get; set; }

}
