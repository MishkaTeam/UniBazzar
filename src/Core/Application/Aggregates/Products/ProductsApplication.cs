using Application.Aggregates.Products.ViewModels;
using Domain;
using Domain.Aggregates.PriceLists;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Products;

public partial class ProductsApplication
    (IProductRepository productRepository,
    IPriceListRepository priceListRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<ProductViewModel> CreateProductAsync(CreateProductViewModel viewModel)
    {
        var product = Product.Create(viewModel.Name, viewModel.ShortDescription, viewModel.FullDescription,
                                    viewModel.StoreId, viewModel.CategoryId, viewModel.UnitId,
                                    viewModel.ProductType, viewModel.DownloadUrl);

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return product.Adapt<ProductViewModel>();
    }

    public async Task<List<ProductViewModel>> GetProducts()
    {
        var products =
            await productRepository.GetAllAsync();

        return products.Adapt<List<ProductViewModel>>();
    }

    public async Task<ProductViewModel> GetProductAsync(Guid id)
    {
        var product =
            await productRepository.GetByIdAsync(id);

        return product.Adapt<ProductViewModel>();
    }
    public async Task<ProductViewModel> GetProductAsync(string Sku)
    {
        var product =
            await productRepository.GetAsync(x => x.SKU == Sku);

        return product.Adapt<ProductViewModel>();
    }

    public async Task<ProductViewModel> UpdateProductAsync(UpdateProductViewModel updateViewModel)
    {
        var productForUpdate =
            await productRepository.GetByIdAsync(updateViewModel.Id);

        if (productForUpdate == null || productForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.Product);

            throw new Exception(message);
        }

        productForUpdate.Update(updateViewModel.Name, updateViewModel.ShortDescription, updateViewModel.FullDescription,
                                updateViewModel.StoreId, updateViewModel.CategoryId, updateViewModel.UnitId,
                                updateViewModel.ProductType, updateViewModel.DownloadUrl);

        await unitOfWork.SaveChangesAsync();
        return productForUpdate.Adapt<ProductViewModel>();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var productForDelete =
            await productRepository.GetByIdAsync(id);

        if (productForDelete == null || productForDelete.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.Product);

            throw new Exception(message);
        }

        await productRepository.RemoveAsync(productForDelete);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ProductCardViewModel>> GetFullProductData(string categorySlug,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetFullProductData(categorySlug, cancellationToken);
        var priceLists = await priceListRepository.GetPrice(products.Select(x => x.Id).ToList());

        return products.Select(x => new ProductCardViewModel
        {
            Price = priceLists?.FirstOrDefault(p => p.productId == x.Id).price ?? 0,
            ImageUrl = x.ProductImages.FirstOrDefault()?.ImageUrl,
            Name = x.Name,
            SKU = x.SKU,
            Slug = x.Slug,
        }).ToList();
    }

    public async Task<List<ProductCardViewModel>> GetIndexProducts()
    {
        var products = await productRepository.GetFullProductData();
        var priceLists = await priceListRepository.GetPrice(products.Select(x => x.Id).ToList());

        return products.Select(x => new ProductCardViewModel
        {
            Price = priceLists?.FirstOrDefault(p => p.productId == x.Id).price ?? 0,
            ImageUrl = x.ProductImages.FirstOrDefault()?.ImageUrl,
            Name = x.Name,
            SKU = x.SKU,
            Slug = "Slug",
        }).ToList();
    }

    public async Task<ProductDetailViewModel> GetProductDetails(string sku)
    {
        var products = await productRepository.GetFullProductData(sku);
        if (products == null)
            throw new Exception();

        var priceLists = await priceListRepository.GetPrice([products.Id]);

        return new ProductDetailViewModel
        {
            Price = priceLists?.FirstOrDefault(p => p.productId == products.Id).price ?? 0,
            Images = [.. products.ProductImages.Select(x => x.ImageUrl)],
            Name = products.Name,
            SKU = products.SKU,
            ShortDescription = products.ShortDescription,
            FullDescription = products.FullDescription,
            ProductFeatures = [.. products.ProductFeatures.Select (x => new ProductFeatureViewModel
            {
                IsPinned = x.IsPinned,
                Key = x.Key,
                Value = x.Value,
            })],
            ProductAttributes = [.. products.ProductAttributes.Select(x => new ProductAttributeViewModel
            {
                AttributeValues = [.. x.Attribute.AttributeValues.Select (x => new ProductAttributeValuesViewModel
                {
                    IsPreSelected = x.IsPreSelected,
                    Name = x.Name,
                    PriceAdjustment = x.PriceAdjustment,
                    WeightAdjustment = x.WeightAdjustment,
                })],
                Name = x.Attribute.Name,
                Description = x.Attribute.Description,
                ProductAttributeType = x.ProductAttributeType,
            })],
            Slug = products.Slug,
        };
    }
}