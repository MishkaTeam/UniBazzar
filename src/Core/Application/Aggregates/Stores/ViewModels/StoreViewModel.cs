using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Stores.ViewModels;

public class StoreViewModel : CreateStoreViewModel
{
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }
}