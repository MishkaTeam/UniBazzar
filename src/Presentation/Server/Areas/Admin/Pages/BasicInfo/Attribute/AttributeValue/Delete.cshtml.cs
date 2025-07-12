using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels;
using Application.Aggregates.Attribute.ViewModels.AttributeValues;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute.AttributeValue;

public class DeleteModel(AttributeApplication attributeApplication) : BasePageModel
{
    [BindProperty]
    public AttributeValueViewModel DeleteViewModel { get; set; } = new();

    public async Task OnGet(Guid attributeId, Guid attributeValueId)
    {
        DeleteViewModel = (await attributeApplication.GetAttributeValue(attributeId, attributeValueId)).Data!;
        DeleteViewModel.AttributeId = attributeId;
    }
    public async Task<IActionResult> OnPost()
    {
        await attributeApplication.RemoveAttributeValue(DeleteViewModel.AttributeId, DeleteViewModel.Id);

        return RedirectToPage("Index",
            new { attributeId = DeleteViewModel.AttributeId });
    }
}
