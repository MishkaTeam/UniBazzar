using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;
using Resources;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.SiteSettings;

public class SiteSetting : Entity
{
    public SiteSetting() 
    {
        // FOR EF!
    }

    public string Description { get; private set; }
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string LogoURL { get; private set; }
    public Guid? PriceListID { get; private set; }
    public string Address { get; private set; }

    private SiteSetting(string name, string description, string phoneNumber, string logoUrl, 
        Guid? priceListId, string address)
    {
        Name = name;
        Description = description;
        PhoneNumber = phoneNumber;
        LogoURL = logoUrl;
        PriceListID = priceListId;
        Address = address;
    }

    public static SiteSetting Create(string name, string description, string phoneNumber, 
        string logoUrl, Guid? priceListId, string address)
    {
        ValidatePhoneNumber(phoneNumber);
        ValidateRequiredFields(name, description, address);

        var siteSetting = new SiteSetting(name, description, phoneNumber, logoUrl, 
            ValidatePriceListId(priceListId), address)
        {
            Name = name.Fix() ?? "",
            Description = description.Fix() ?? "",
            PhoneNumber = phoneNumber.Fix() ?? "",
            LogoURL = logoUrl.Fix() ?? "",
            Address = address.Fix() ?? "",
            PriceListID = ValidatePriceListId(priceListId)
        };

        return siteSetting;
    }

    public void Update(string name, string description, string phoneNumber, 
        string logoUrl, Guid? priceListId, string address)
    {
        ValidatePhoneNumber(phoneNumber);
        ValidateRequiredFields(name, description, address);

        Name = name.Fix() ?? "";
        Description = description.Fix() ?? "";
        PhoneNumber = phoneNumber.Fix() ?? "";
        LogoURL = logoUrl.Fix() ?? "";
        Address = address.Fix() ?? "";
        PriceListID = ValidatePriceListId(priceListId);

        SetUpdateDateTime();
    }

    private static void ValidatePhoneNumber(string phoneNumber)
    {
        if (!phoneNumber.IsValidMobile())
        {
            var message = string.Format(Validations.Required, DataDictionary.CellPhonenumber);
            throw new ValidationException(message);
        }
    }

    private static void ValidateRequiredFields(string name, string description, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            var message = string.Format(Validations.Required, DataDictionary.Name);
            throw new ArgumentException(message);
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            var message = string.Format(Validations.Required, DataDictionary.FullDescription);
            throw new ArgumentException(message);
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            var message = string.Format(Validations.Required, DataDictionary.Address);
            throw new ArgumentException(message);
        }
    }

    private static Guid? ValidatePriceListId(Guid? priceListId)
    {
        return priceListId == Guid.Empty ? null : priceListId;
    }
}
