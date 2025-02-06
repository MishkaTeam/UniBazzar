using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Domain.Aggregates.Products.ProductPriceLists;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public partial class ProductsApplication
{
    public async Task<ProductPriceListViewModel> CreateProductPriceListAsync(ProductPriceListViewModel viewModel)
    {
        var productPricelist = ProductPriceList.Create(viewModel.ProductId, viewModel.Price);

        productPriceListRepository.AddProductPriceList(productPricelist);

        await unitOfWork.CommitAsync();
        return productPricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task<ProductPriceListViewModel> GetProductPriceListAsync(Guid id)
    {
        var productPricelist = productPriceListRepository.GetProductPriceListAsync(id);

        return productPricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task<ProductPriceListViewModel> GetPriceByProductId(Guid id)
    {
        var productPricelist = productPriceListRepository.GetPriceByProductId(id);

        return productPricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task<List<ProductPriceListViewModel>> GetPriceListByProductId(Guid id)
    {
        var productPricelist = productPriceListRepository.GetPriceListByProductId(id);

        return productPricelist.Adapt<List<ProductPriceListViewModel>>();
    }

    public async Task<List<ProductPriceListViewModel>> GetAllProductPriceListAsync()
    {
        var productPricelist = productPriceListRepository.GetAllProductPriceListAsync();

        return productPricelist.Adapt<List<ProductPriceListViewModel>>();
    }

    public async Task<ProductPriceListViewModel> UpdateProductPriceListAsync(ProductPriceListViewModel updateViewModel)
    {
        var productPricelist = await productPriceListRepository.GetProductPriceListAsync(updateViewModel.Id);

        if (productPricelist == null || updateViewModel.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productPricelist.Update(updateViewModel.ProductId, updateViewModel.Price);

        await unitOfWork.CommitAsync();
        return productPricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task DeleteProductPriceListAsync(Guid id)
    {
        var productPricelistForDelete =
            await productPriceListRepository.GetProductPriceListAsync(id);

        if (productPricelistForDelete == null || productPricelistForDelete.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productPriceListRepository.Remove(productPricelistForDelete);
        await unitOfWork.CommitAsync();
    }
}
