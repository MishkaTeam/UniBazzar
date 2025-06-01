using Application.Aggregates.Products.ProductImages.ViewModel;
using Domain;
using Domain.Aggregates.Products.ProductImages;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public class ProductImagesApplication(IProductImageRepository productImageRepository, IUnitOfWork unitOfWork)
{
    public async Task CreateProductImage(CreateProductImageViewModel viewModel)
    {
        var productImage = ProductImage.Create(viewModel.ProductId, viewModel.ImageUrl);
        await productImageRepository.AddAsync(productImage);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<ProductImageViewModel> GetProductImageAsync(Guid id)
    {
        var productImage = await productImageRepository.GetByIdAsync(id);
        return productImage.Adapt<ProductImageViewModel>();
    }

    public async Task<List<ProductImageViewModel>> GetAllProductImagesAsync()
    {
        var productImage = await productImageRepository.GetAllAsync();
        return productImage.Adapt<List<ProductImageViewModel>>();
    }

    public async Task<List<ProductImageViewModel>> GetImageByProductIdAsync(Guid productid)
    {
        var productImage = await productImageRepository.GetImageByProductIdAsync(productid);
        return productImage.Adapt<List<ProductImageViewModel>>();
    }

    public async Task<ProductImageViewModel> UpdateProductImage(ProductImageViewModel viewModel)
    {
        var productImageForUpdate = await productImageRepository.GetByIdAsync(viewModel.Id);

        if (productImageForUpdate == null || productImageForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ProductFeature);

            throw new Exception(message);
        }

        productImageForUpdate.Update(viewModel.ProductId, viewModel.ImageUrl);

        await unitOfWork.SaveChangesAsync();

        return productImageForUpdate.Adapt<ProductImageViewModel>();
    }

    public async Task DeleteImage(Guid id)
    {
        var productImageForDelete = await productImageRepository.GetByIdAsync(id);

        if (productImageForDelete == null || productImageForDelete.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productImageRepository.RemoveAsync(productImageForDelete);

        await unitOfWork.SaveChangesAsync();
    }

}
