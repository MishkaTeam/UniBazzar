using Domain.Aggregates.Categories;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Attribute.ViewModels;

public class CreateAttributeViewModel
{
    
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Name))]
    public string Name { get;  set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.FullDescription))]
    public string Description { get;  set; }

    public Guid CategoryId { get; set; }

}
