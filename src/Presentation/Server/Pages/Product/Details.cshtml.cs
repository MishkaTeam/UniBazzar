using Application.Aggregates.Customers;
using Application.Aggregates.Ordering.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;
using Application.Aggregates.Ordering.Baskets.ViewModels.InitializeBasket;
using Application.Aggregates.ProductReviews;
using Application.Aggregates.ProductReviews.ViewModels;
using Application.Aggregates.Products;
using Application.Aggregates.Products.Basket;
using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Products.ViewModels;
using BuildingBlocks.Domain.Context;
using Constants;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Infrastructure.Extentions;

namespace Server.Pages;

public class DetailModel(ProductsApplication productsApplication,
                          ProductImagesApplication productImagesApplication,
                          ProductFeaturesApplication productFeaturesApplication,
                          ProductReviewApplication productReviewApplication,
                          CustomerApplication customerApplication,
                          BasketApplication basketApplication,
                          IExecutionContextAccessor executionContextAccessor) : PageModel
{

    public ProductDetailViewModel ProductDetail { get; set; } = new();

    [BindProperty]
    public CreateProductReviewViewModel CreateCommentViewModel { get; set; } = new();

    [BindProperty]
    public AddBasketProductViewModel BasketProduct { get; set; } = new();

    public List<DetailsProductReviewViewModel> ViewModel { get; set; } = [];
    public async Task<IActionResult> OnGetAsync(string sku, string slug)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            return RedirectToPage("Error/Error404");
        }

        await GetProductInformation(sku);

        return Page();
    }

    private async Task GetProductInformation(string sku)
    {
        ProductDetail = await productsApplication.GetProductDetails(sku);

        ProductDetail.ProductAttributes.ForEach(x => BasketProduct.SelectedAttributes.TryAdd(x.Id, Guid.Empty));
        BasketProduct.ProductId = ProductDetail.Id;

        CreateCommentViewModel.ProductId = ProductDetail.Id;

        ViewModel = await productReviewApplication.GetProductReviewsByProductSkuAsync(sku);

        ViewModel = ViewModel.Where(x => x.IsVerified).ToList();
    }

    public async Task<IActionResult> OnPostCommentAsync(string sku, string slug)
    {
        if (User.Identity == null
           || User.Identity.IsAuthenticated == false
           || executionContextAccessor.UserId is null)
        {
            return RedirectToRoute("/Account/Login", new { returnUrl = HttpContext.GetUrl() });
        }
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CreateCommentViewModel.CustomerId = executionContextAccessor.UserId!.Value;

        CreateCommentViewModel.IsVerified = false;

        await productReviewApplication.Create(CreateCommentViewModel);

        CreateCommentViewModel = new();

        return RedirectToPage("Details", new { sku = sku, slug = slug });
    }

    public async Task<IActionResult> OnPostPurchaseAsync(string sku)
    {
        await GetProductInformation(sku);
        BasketProduct.BasketId = await TryGetBasketId();
        var attributes = new List<BasketItemAttributeContract>();
        foreach (var item in BasketProduct.SelectedAttributes.Where(x => x.Value != Guid.Empty))
        {
            var pAttribute = ProductDetail.ProductAttributes.FirstOrDefault(x => x.Id == item.Key);
            if (pAttribute == null)
                continue;

            var pAttributeValue = pAttribute.AttributeValues.FirstOrDefault(x => x.Id == item.Value);
            if (pAttribute == null || pAttributeValue == null)
                continue;


            attributes.Add(new BasketItemAttributeContract
            {
                PriceAdjustment = pAttributeValue.PriceAdjustment,
                ProductAttributeId = pAttribute.Id,
                ProductAttributeName = pAttribute.Name,
                ProductAttributeValue = pAttributeValue.Name,
                ProductAttributeValueId = pAttributeValue.Id
            });
        }
        await basketApplication.AddItem(new AddBasketItemRequestModel
        {
            Quantity = 1,
            ProductName = ProductDetail.Name,
            BasePrice = ProductDetail.Price,
            BasketId = BasketProduct.BasketId.Value,
            DiscountAmount = ProductDetail.Discount,
            DiscountType = DiscountType.Price,
            ProductId = BasketProduct.ProductId,
            BasketItemAttributes = attributes
        });
        return Page();

    }

    private async Task<Guid?> TryGetBasketId()
    {
        var basketId = Request.Cookies.FirstOrDefault(x => x.Key == BasketConstants.BASKET);
        if (string.IsNullOrEmpty(basketId.Value))
        {
            var basket = await basketApplication.InitializeBasket(new InitializeBasketRequestModel
            {
                Platform = Platform.Ecommerce
            });

            if (basket != null && basket.IsSuccessful && basket.Data is not null)
            {
                Response.Cookies.Append(BasketConstants.BASKET, basket.Data.Id.ToString(), new CookieOptions
                {
                    HttpOnly = false,
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
                return basket.Data?.Id;
            }
        }
        return Guid.Parse(basketId.Value);
    }
}
