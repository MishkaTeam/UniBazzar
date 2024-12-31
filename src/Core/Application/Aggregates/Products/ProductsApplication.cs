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
									viewModel.StoreId, viewModel.CategoryId, viewModel.BrandId, viewModel.UnitId, viewModel.ActivePriceListId,
									viewModel.ProductType, viewModel.DownloadUrl);

		productRepository.AddProduct(product);
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

	public async Task<ProductViewModel> UpdateProductAsync(ProductViewModel updateViewModel)
	{
		var productForUpdate =
			await productRepository.GetProductAsync(updateViewModel.Id);

		if (productForUpdate == null || productForUpdate.Id == Guid.Empty)
		{
			throw new Exception(Errors.NotFound);
		}

		productForUpdate.Update(productForUpdate.Name, productForUpdate.ShortDescription, productForUpdate.FullDescription,
								productForUpdate.StoreId, productForUpdate.CategoryId, productForUpdate.BrandId, productForUpdate.UnitId, productForUpdate.ActivePriceListId,
								productForUpdate.ProductType, productForUpdate.DownloadUrl);

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