using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Branches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Comments;
using Application.Aggregates.Comments.ViewModels;

namespace Server.Areas.Admin.Pages.BasicInfo.Comments;

public class IndexModel(CommentApplication commentApplication) : PageModel
{
    public List<UpdateCommentViewModel> ViewModel { get; set; }
    public async Task OnGet()
    {
        ViewModel = await commentApplication.GetAllCommentsAsync();
    }
}
