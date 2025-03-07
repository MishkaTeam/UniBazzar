using Application.Aggregates.Products.ProductImages.ViewModel;
using Domain.Aggregates.Products.ProductImages;
using Mapster;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.Products;

public partial class ProductsApplication
{
    public async Task CreateProductImage(CreateProductImageViewModel viewModel)
    {
        var productImage = ProductImage.Create(viewModel.ProductId, viewModel.ImageUrl);
        await productRepository.AddProductImage(productImage);
        await unitOfWork.CommitAsync();
    }

    public async Task<ProductImageViewModel> GetProductImageAsync(Guid id)
    {
        var productImage = await productRepository.GetProductImageAsync(id);
        return productImage.Adapt<ProductImageViewModel>();
    }

    public async Task<List<ProductImageViewModel>> GetAllProductImagesAsync()
    {
        var productImage = await productRepository.GetAllProductImagesAsync();
        return productImage.Adapt<List<ProductImageViewModel>>();
    }

    public async Task<List<ProductImageViewModel>> GetImageByProductIdAsync(Guid productid)
    {
        var productImage = await productRepository.GetImageByProductIdAsync(productid);
        return productImage.Adapt<List<ProductImageViewModel>>();
    }

    public async Task<ProductImageViewModel> UpdateProductImage(ProductImageViewModel viewModel)
    {
        var productImageForUpdate = await productRepository.GetProductImageAsync(viewModel.Id);

        if (productImageForUpdate == null || productImageForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ProductFeature);

            throw new Exception(message);
        }

        productImageForUpdate.Update(viewModel.ProductId, viewModel.ImageUrl);

        await unitOfWork.CommitAsync();

        return productImageForUpdate.Adapt<ProductImageViewModel>();
    }

    public async Task DeleteImage(Guid id)
    {
        var productImageForDelete = await productRepository.GetProductImageAsync(id);

        if (productImageForDelete == null || productImageForDelete.Id == Guid.Empty)
        {
            throw new Exception(Errors.NotFound);
        }

        productRepository.RemoveImage(productImageForDelete);

        await unitOfWork.CommitAsync();
    }

}
