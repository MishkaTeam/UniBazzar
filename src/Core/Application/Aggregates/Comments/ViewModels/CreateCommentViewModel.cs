using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Comments.ViewModels;

public class CreateCommentViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Text))]
    public string Text { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductId { get; set; }
}
