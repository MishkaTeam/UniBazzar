using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels.AttributeValues;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute.AttributeValue;

public class UpdateModel(
    AttributeApplication attributeApplication) : BasePageModel
{
    [BindProperty]
    public UpdateAttributeValueViewModel UpdateViewModel { get; set; } = new();

    public async Task OnGet(Guid attributeId, Guid attributeValueId)
    {
        UpdateViewModel =
            (await attributeApplication.GetAttributeValue(attributeId, attributeValueId)).Data!;
        UpdateViewModel.AttributeId = attributeId;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        await attributeApplication.UpdateAttributeValue(UpdateViewModel);

        return RedirectToPage("Index",
            new { attributeId = UpdateViewModel.AttributeId });
    }
}
