
using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute
{
    public class IndexModel(AttributeApplication attributeApplication) : BasePageModel
    {
        public List<UpdateAttributeViewModel> ViewModel { get; set; } = [];
        public async Task OnGet()
        {
            ViewModel = await attributeApplication.GetAllAttributeAsync();
        }
    }
}
