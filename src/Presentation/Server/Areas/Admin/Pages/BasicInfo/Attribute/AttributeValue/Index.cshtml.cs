using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels.AttributeValues;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute.AttributeValue;

public class IndexModel(AttributeApplication attributeApplication) : BasePageModel
{
    public List<AttributeValueViewModel> ViewModel { get; set; } = [];
    public async Task OnGet(Guid attributeId)
    {
        ViewModel = (await attributeApplication.GetAttributeValues(attributeId)).Data!;

    }
}
