using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Users;

public class UpdateUserViewModel : CreateUserViewModel
{
    [Display(ResourceType = typeof(Resources.DataDictionary),
       Name = nameof(Resources.DataDictionary.Id))]
    public Guid Id { get; set; }

}
