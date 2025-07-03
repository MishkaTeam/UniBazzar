using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels;
using Application.Aggregates.Customers;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute
{
    public class DeleteModel(AttributeApplication attributeApplication) : BasePageModel
    {
        [BindProperty]
        public UpdateAttributeViewModel DeleteViewModel { get; set; } = new();

        public async Task OnGet(Guid Id)
        {
            DeleteViewModel = await attributeApplication.GetAttributeAsync(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await attributeApplication.DeleteAsync(DeleteViewModel.Id);
            return RedirectToPage("Index");
        }
    }
}
