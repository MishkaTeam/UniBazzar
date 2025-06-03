using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Comments.ViewModels;
using Domain;
using Domain.Aggregates.Comments;
using Mapster;

namespace Application.Aggregates.Comments;

public class CommentApplication(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
{
    public async Task<Comment> Create(CreateCommentViewModel viewModel)
    {
        var comment = Comment.Create
            (
            viewModel.Text,
            viewModel.CustomerId,
            viewModel.ProductId
            );

        await commentRepository.AddAsync(comment);
        await unitOfWork.SaveChangesAsync();

        return comment.Adapt<Comment>();
    }

    public async Task<List<UpdateCommentViewModel>> GetAllCommentsAsync()
    {
        var comment = await commentRepository.GetAllAsync();

        return comment.Adapt<List<UpdateCommentViewModel>>();
    }

    public async Task<UpdateCommentViewModel> GetCommentAsync(Guid id)
    {
        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null || comment.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }
        return comment.Adapt<UpdateCommentViewModel>();
    }

    public async Task<UpdateCommentViewModel> UpdateAsync(UpdateCommentViewModel updateViewModel)
    {
        var comment = await commentRepository.GetByIdAsync(updateViewModel.Id);

        if (comment == null || comment.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        comment.Update
            (
             updateViewModel.Text
            );

        await unitOfWork.SaveChangesAsync();

        return comment.Adapt<UpdateCommentViewModel>();
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
