using Application.Aggregates.Products.ViewModels;
using Domain;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public partial class ProductsApplication
	(IProductRepository productRepository, 
	IProductFeatureRepository productFeatureRepository,
	IUnitOfWork unitOfWork)
{
	public async Task<ProductViewModel> CreateProductAsync(CreateProductViewModel viewModel)
	{
		var product = Product.Create(viewModel.Name, viewModel.ShortDescription, viewModel.FullDescription,
									viewModel.StoreId, viewModel.CategoryId, viewModel.UnitId,
									viewModel.ProductType, viewModel.DownloadUrl);

		await productRepository.AddProductAsync(product);
		await unitOfWork.CommitAsync();

		return product.Adapt<ProductViewModel>();
	}

	public async Task<List<ProductViewModel>> GetProducts()
	{
		var products =
			await productRepository.GetAllProductsAsync();

		return products.Adapt<List<ProductViewModel>>();
	}

	public async Task<ProductViewModel> GetProductAsync(Guid id)
	{
		var product =
			await productRepository.GetProductAsync(id);

		return product.Adapt<ProductViewModel>();
	}

	public async Task<ProductViewModel> UpdateProductAsync(UpdateProductViewModel updateViewModel)
	{
		var productForUpdate =
			await productRepository.GetProductAsync(updateViewModel.Id);

		if (productForUpdate == null || productForUpdate.Id == Guid.Empty)
		{
			var message =
				string.Format(Errors.NotFound, Resources.DataDictionary.Product);

			throw new Exception(message);
		}

		productForUpdate.Update(updateViewModel.Name, updateViewModel.ShortDescription, updateViewModel.FullDescription,
								updateViewModel.StoreId, updateViewModel.CategoryId, updateViewModel.UnitId,
								updateViewModel.ProductType, updateViewModel.DownloadUrl);

		await unitOfWork.CommitAsync();
		return productForUpdate.Adapt<ProductViewModel>();
	}

	public async Task DeleteProductAsync(Guid id)
	{
		var productForDelete =
			await productRepository.GetProductAsync(id);

		if (productForDelete == null || productForDelete.Id == Guid.Empty)
		{
			throw new Exception(Errors.NotFound);
		}

		productRepository.RemoveProduct(productForDelete);
		await unitOfWork.CommitAsync();
	}
}