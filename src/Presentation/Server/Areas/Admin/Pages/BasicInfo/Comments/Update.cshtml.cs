using Application.Aggregates.Comments;
using Application.Aggregates.Comments.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Comments
{
    public class UpdateModel(CommentApplication commentApplication) : PageModel
    {
        [BindProperty]
        public UpdateCommentViewModel UpdateViewModel { get; set; } = new();
        public async Task OnGet(Guid Id)
        {
            UpdateViewModel = await commentApplication.GetCommentAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await commentApplication.UpdateAsync(UpdateViewModel);
            return RedirectToPage("Index");
        }
    }
}
