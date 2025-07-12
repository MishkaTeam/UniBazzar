using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels.AttributeValues;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute.AttributeValue;

public class CreateModel(
    AttributeApplication attributeApplication) : BasePageModel
{

    [BindProperty]
    public CreateAttributeItemRequestModel CreateViewModel { get; set; } = new();

    public async Task OnGet(Guid attributeId)
    {
        CreateViewModel.AttributeId = attributeId;
    }
    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            await attributeApplication.AddValue(CreateViewModel);
        }
        return RedirectToPage("Index",
            new { attributeId = CreateViewModel.AttributeId });
    }
}
