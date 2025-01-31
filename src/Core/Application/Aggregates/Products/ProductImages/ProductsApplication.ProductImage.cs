using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ProductImages.ViewModels;
using Domain.Aggregates.Products.ProductImages;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public partial class ProductsApplication
{
    public async Task<ProductImageViewModel> CreateProductImageAsync(CreateProductImageViewModel viewModel)
    {
        var productImage = ProductImage.Create(viewModel.ProductId, viewModel.ImageUrl);

        productRepository.AddProductImage(productImage);

        await unitOfWork.CommitAsync();
        return productImage.Adapt<ProductImageViewModel>();
    }

    public async Task<ProductImageViewModel> GetProductImageAsync(Guid id)
    {
        var productImage = productImageRepository.GetProductImageAsync(id);

        return productImage.Adapt<ProductImageViewModel>();
    }

    public async Task<List<ProductImageViewModel>> GetAllProductImageAsync()
    {
        var productImage = productImageRepository.GetAllProductImagesAsync();

        return productImage.Adapt<List<ProductImageViewModel>>();
    }

    public async Task<ProductImageViewModel> GetImageByProductIdAsync(Guid id)
    {
        var productImage = productImageRepository.GetImageByProductIdAsync(id);

        return productImage.Adapt<ProductImageViewModel>();
    }

    public async Task<ProductImageViewModel> UpdateProductImageAsync(ProductImageViewModel updateViewModel)
    {
        var productImageForUpdate =
            await productImageRepository.GetProductImageAsync(updateViewModel.Id);

        if (productImageForUpdate == null || productImageForUpdate.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productImageForUpdate.Update(
            updateViewModel.ProductId, updateViewModel.ImageUrl);

        await unitOfWork.CommitAsync();
        return productImageForUpdate.Adapt<ProductImageViewModel>();
    }

    public async Task DeleteProductImageAsync(Guid id)
    {
        var productImageForDelete =
            await productImageRepository.GetProductImageAsync(id);

        if (productImageForDelete == null || productImageForDelete.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productImageRepository.Remove(productImageForDelete);
        await unitOfWork.CommitAsync();
    }
}
