using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Branches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Comments.ViewModels;
using Application.Aggregates.Comments;

namespace Server.Areas.Admin.Pages.BasicInfo.Comments
{
    public class DeleteModel(CommentApplication commentApplication) : PageModel
    {

        [BindProperty]
        public UpdateCommentViewModel DeleteViewModel { get; set; } = new();
        public async Task OnGet(Guid Id)
        {
            DeleteViewModel = await commentApplication.GetCommentAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await commentApplication.DeleteAsync(DeleteViewModel.Id);
            return RedirectToPage("Index");
        }
    }
}
