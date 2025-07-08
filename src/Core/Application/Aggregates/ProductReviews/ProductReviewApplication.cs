using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.ProductReviews.ViewModels;
using Domain;
using Domain.Aggregates.ProductReviews;
using Mapster;

namespace Application.Aggregates.ProductReviews;

public class ProductReviewApplication(IProductReviewRepository commentRepository, IUnitOfWork unitOfWork)
{
    public async Task<ProductReview> Create(CreateProductReviewViewModel viewModel)
    {
        var comment = ProductReview.Create
            (
            viewModel.Text,
            viewModel.CustomerId,
            viewModel.ProductId,
            viewModel.Rate,
            viewModel.IsVerified
            );

        await commentRepository.AddAsync(comment);
        await unitOfWork.SaveChangesAsync();

        return comment.Adapt<ProductReview>();
    }

    public async Task<List<DetailsProductReviewViewModel>> GetAllCommentsAsync()
    {
        var comment = await commentRepository.GetAllAsync();

        return comment.Adapt<List<DetailsProductReviewViewModel>>();
    }
    public async Task<List<DetailsProductReviewViewModel>> GetProductReviewsByProductSkuAsync(string sku)
    {
        var comment = await commentRepository.GetProductReviewsByProductSkuAsync(sku);

        return comment.Adapt<List<DetailsProductReviewViewModel>>();  
    }

    public async Task<DetailsProductReviewViewModel> GetCommentAsync(Guid id)
    {
        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null || comment.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }
        return comment.Adapt<DetailsProductReviewViewModel>();
    }

    public async Task<UpdateProductReviewViewModel> UpdateAsync(UpdateProductReviewViewModel updateViewModel)
    {
        var comment = await commentRepository.GetByIdAsync(updateViewModel.Id);

        if (comment == null || comment.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        comment.Update
            (
             updateViewModel.Text,
             updateViewModel.IsVerified
            );

        await unitOfWork.SaveChangesAsync();

        return comment.Adapt<UpdateProductReviewViewModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null || comment.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await commentRepository.RemoveAsync(comment);
        await unitOfWork.SaveChangesAsync();
    }
}
