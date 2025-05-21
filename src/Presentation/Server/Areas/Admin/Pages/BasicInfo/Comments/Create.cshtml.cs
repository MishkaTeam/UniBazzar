using Application.Aggregates.Branches.ViewModels;
using Application.Aggregates.Branches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Comments;
using Application.Aggregates.Comments.ViewModels;

namespace Server.Areas.Admin.Pages.BasicInfo.Comments
{
    public class CreateModel(CommentApplication commentApplication) : PageModel
    {

        [BindProperty]
        public CreateCommentViewModel ViewModel { get; set; } = new();

        public async Task OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await commentApplication.Create(ViewModel);
            }
            return RedirectToPage("Index");
        }
    }
}
