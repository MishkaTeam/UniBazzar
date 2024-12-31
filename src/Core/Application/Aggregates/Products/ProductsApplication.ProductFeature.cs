using Application.Aggregates.Products.ViewModels.ProductFeatures;
using Domain.Aggregates.Products.ProductFeatures;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public partial class ProductsApplication
{
	public async Task<ProductFeatureViewModel> CreateProductFeatureAsync(CreateProductFeatureViewModel viewModel)
	{
		var productFeature = ProductFeature.Create(
				viewModel.ProductId, viewModel.Key,
				viewModel.Value, viewModel.IsPinned, viewModel.Order);

		productRepository.AddProductFeature(productFeature);
		await unitOfWork.CommitAsync();
		return productFeature.Adapt<ProductFeatureViewModel>();
	}

	public async Task<List<ProductFeatureViewModel>> GetProductFeatures(Guid productId)
	{
		var productFeatures =
			await productFeatureRepository.GetAllProductFeaturesAsync(productId);

		return productFeatures.Adapt<List<ProductFeatureViewModel>>();
	}

	public async Task<ProductFeatureViewModel> GetProductFeatureAsync(Guid id)
	{
		var productFeature =
			await productFeatureRepository.GetProductFeatureAsync(id);

		return productFeature.Adapt<ProductFeatureViewModel>();
	}

	public async Task<ProductFeatureViewModel> UpdateProductFeatureAsync(ProductFeatureViewModel updateViewModel)
	{
		var productFeatureForUpdate =
			await productFeatureRepository.GetProductFeatureAsync(updateViewModel.Id);

		if (productFeatureForUpdate == null || productFeatureForUpdate.Id == Guid.Empty)
		{
			throw new Exception(Errors.NotFound);
		}

		productFeatureForUpdate.Update(
			updateViewModel.Key, updateViewModel.Value,
			updateViewModel.IsPinned, updateViewModel.Order);

		await unitOfWork.CommitAsync();
		return productFeatureForUpdate.Adapt<ProductFeatureViewModel>();
	}

	public async Task DeleteProductFeatureAsync(Guid id)
	{
		var productFeatureForDelete =
			await productFeatureRepository.GetProductFeatureAsync(id);

		if (productFeatureForDelete == null || productFeatureForDelete.Id == Guid.Empty)
		{
			throw new Exception(Errors.NotFound);
		}

		productFeatureRepository.RemoveProductFeature(productFeatureForDelete);
		await unitOfWork.CommitAsync();
	}
}