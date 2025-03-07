using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Domain.Aggregates.Products.ProductPriceLists;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public partial class ProductsApplication
{
    public async Task CreateProductPriceList(CreateProductPriceListViewModel viewModel)
    {
        var productpricelist = ProductPriceList.Create(viewModel.ProductId, viewModel.Price);
        await productRepository.AddProductPriceList(productpricelist);
        await unitOfWork.CommitAsync();
    }

    public async Task<ProductPriceListViewModel> GetProductPriceListAsync(Guid id)
    {
        var productpricelist = await productRepository.GetProductPriceListAsync(id);
        return productpricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task<List<ProductPriceListViewModel>> GetAllProductPriceListAsync()
    {
        var productpricelist = await productRepository.GetAllProductPriceListAsync();
        return productpricelist.Adapt<List<ProductPriceListViewModel>>();
    }

    public async Task<ProductPriceListViewModel> GetPriceByProductId(Guid productid)
    {
        var productpricelist = await productRepository.GetPriceByProductId(productid);
        return productpricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task<List<ProductPriceListViewModel>> GetPriceListByProductId(Guid productid)
    {
        var productpricelist = await productRepository.GetPriceListByProductId(productid);
        return productpricelist.Adapt<List<ProductPriceListViewModel>>();
    }

    public async Task<ProductPriceListViewModel> UpdatePriceList(ProductPriceListViewModel model)
    {
        var productpricelistForUpdate = await productRepository.GetProductPriceListAsync(model.Id);

        if (productpricelistForUpdate == null || productpricelistForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ProductFeature);

            throw new Exception(message);
        }

        productpricelistForUpdate.Update(model.ProductId, model.Price);

        await unitOfWork.CommitAsync();

        return productpricelistForUpdate.Adapt<ProductPriceListViewModel>();

    }

    public async Task DeleteProductPriceList(Guid id)
    {
        var productpricelistForDelete = await productRepository.GetProductPriceListAsync(id);

        if (productpricelistForDelete == null || productpricelistForDelete.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productRepository.RemovePriceList(productpricelistForDelete);

        await unitOfWork.CommitAsync();
    }

}
