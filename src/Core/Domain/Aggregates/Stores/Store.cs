using Framework.DataType;
using Resources.Messages;
using System.ComponentModel.DataAnnotations.Schema;
using DateTimeUtility = Domain.BuildingBlocks.SeedWork.DateTimeUtility;
using IEntityHasOwner = Domain.BuildingBlocks.SeedWork.IEntityHasOwner;
using IEntityHasUpdateInfo = Domain.BuildingBlocks.SeedWork.IEntityHasUpdateInfo;
using IsEntityHasVersionControl = Domain.BuildingBlocks.SeedWork.IsEntityHasVersionControl;

namespace Domain.Aggregates.Stores;

public class Store :
    IsEntityHasVersionControl,
    IEntityHasUpdateInfo,
    IEntityHasOwner

{
    public Store()
    {
        //For EF 
    }

    private Store
        (string name, string? description, string phoneNumber,
        string address, string? culture, string? logoUrl, bool isActive)
    {
        Id = Guid.NewGuid();

        Name = name;
        Description = description;
        PhoneNumber = phoneNumber;
        HostUrl = address;
        Culture = culture;
        LogoUrl = logoUrl;
        IsActive = isActive;

        SetInsertDateTime();
    }


    [DatabaseGenerated
    (DatabaseGeneratedOption.None)]
    public Guid Id { get; private set; }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string PhoneNumber { get; private set; }
    public string HostUrl { get; private set; }
    public string? Culture { get; private set; }
    public string? LogoUrl { get; private set; }
    public bool IsActive { get; private set; }

    public int Version { get; private set; }
    public int Ordering { get; private set; }

    public Guid OwnerId { get; private set; }
    public Guid InsertedBy { get; private set; }
    public Guid UpdatedBy { get; private set; }

    public long InsertDateTime { get; private set; }
    public long UpdateDateTime { get; private set; }

    public static Store Create
        (string name, string? description, string phoneNumber,
        string hostUrl, string? culture, string? logoUrl, bool isActive)
    {
        ValidatePhoneNumber(phoneNumber);

        var store =
            new Store(name, description, phoneNumber,
            hostUrl, culture, logoUrl, isActive)
            {
                Name = name.Fix() ?? "",
                Description = description.Fix() ?? "",
                PhoneNumber = phoneNumber,
                HostUrl = hostUrl.Fix() ?? "",
                Culture = culture.Fix() ?? "",
                LogoUrl = logoUrl.Fix() ?? ""
            };

        return store;
    }

    public void Update
        (string name, string? description, string phoneNumber, string? culture, string? logoUrl)
    {
        ValidatePhoneNumber(phoneNumber);

        Name = name.Fix() ?? "";
        Description = description.Fix() ?? "";
        PhoneNumber = phoneNumber;
        Culture = culture.Fix() ?? "";
        LogoUrl = logoUrl.Fix() ?? "";

        SetUpdateDateTime();
    }


    private static void ValidatePhoneNumber(string phoneNumber)
    {
        if (phoneNumber.IsValidMobile() == false)
        {
            var message =
                string.Format(Errors.Invalid, Resources.DataDictionary.CellPhonenumber);

            throw new InvalidDataException(message);
        }
    }

    #region [ Methods ]
    public void SetOwner(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public Guid GetOwner()
    {
        return OwnerId;
    }

    public void SetInsertBy(Guid Id)
    {
        InsertedBy = Id;
    }

    public void SetInsertDateTime()
    {
        InsertDateTime =
            DateTimeUtility.GetCurrentUnixUTCTimeSeconds();
    }

    public void SetUpdateBy(Guid Id)
    {
        InsertedBy = Id;
    }

    public void SetUpdateDateTime()
    {
        UpdateDateTime =
            DateTimeUtility.GetCurrentUnixUTCTimeSeconds();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
    #endregion
}