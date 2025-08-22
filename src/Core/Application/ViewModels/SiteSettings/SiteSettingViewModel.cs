namespace Application.ViewModels.SiteSettings;

public class SiteSettingViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LogoURL { get; set; } = string.Empty;
    public Guid? PriceListID { get; set; }
    public DateTime InsertDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
    public Guid StoreId { get; set; }
    public Guid OwnerId { get; set; }
}
