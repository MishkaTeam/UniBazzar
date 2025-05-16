using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Branches.ViewModels;

public class CreateBranchViewModel 
{
    [Required
    (AllowEmptyStrings = false,
    ErrorMessageResourceType = typeof(Resources.Messages.Validations),
    ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
    public string Name { get; set; }
}
