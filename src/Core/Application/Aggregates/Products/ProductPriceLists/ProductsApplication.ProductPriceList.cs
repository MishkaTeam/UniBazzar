using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Domain;
using Domain.Aggregates.Products.ProductPriceLists;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public class ProductPriceListsApplication(IProductPriceListRepository productPriceList ,IUnitOfWork unitOfWork)
{
    public async Task CreateProductPriceList(CreateProductPriceListViewModel viewModel)
    {
        var productpricelist = ProductPriceList.Create(viewModel.ProductId, viewModel.Price);
        await productPriceList.AddAsync(productpricelist);
        await unitOfWork.CommitAsync();
    }

    public async Task<ProductPriceListViewModel> GetProductPriceListAsync(Guid id)
    {
        var productpricelist = await productPriceList.GetByIdAsync(id);
        return productpricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task<List<ProductPriceListViewModel>> GetAllProductPriceListAsync()
    {
        var productpricelist = await productPriceList.GetAllAsync();
        return productpricelist.Adapt<List<ProductPriceListViewModel>>();
    }

    public async Task<ProductPriceListViewModel> GetPriceByProductId(Guid productid)
    {
        var productpricelist = await productPriceList.GetPriceByProductId(productid);
        return productpricelist.Adapt<ProductPriceListViewModel>();
    }

    public async Task<List<ProductPriceListViewModel>> GetPriceListByProductId(Guid productid)
    {
        var productpricelist = await productPriceList.GetPriceListByProductId(productid);
        return productpricelist.Adapt<List<ProductPriceListViewModel>>();
    }

    public async Task<ProductPriceListViewModel> UpdatePriceList(ProductPriceListViewModel model)
    {
        var productpricelistForUpdate = await productPriceList.GetByIdAsync(model.Id);

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
        var productpricelistForDelete = await productPriceList.GetByIdAsync(id);

        if (productpricelistForDelete == null || productpricelistForDelete.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productPriceList.RemoveAsync(productpricelistForDelete);

        await unitOfWork.CommitAsync();
    }

}
