using BuildingBlocks.Domain.SeedWork;
using Framework.DataType;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates.Stores;

public class Store :
	IsEntityHasVersionControl,
	IEntityHasUpdateInfo,
	IEntityHasTenant,
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
		Address = address;
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
	public string Address { get; private set; }
	public string? Culture { get; private set; }
	public string? LogoUrl { get; private set; }
	public bool IsActive { get; private set; }

	public int Version { get; private set; }
	public int Ordering { get; private set; }

	public Guid OwnerId { get; private set; }
	public Guid TenantId { get; private set; }

	public Guid InsertedBy { get; private set; }
	public Guid UpdatedBy { get; private set; }

	public long InsertDateTime { get; private set; }
	public long UpdateDateTime { get; private set; }

	public static Store Create
		(string name, string? description, string phoneNumber,
		string address, string? culture, string? logoUrl, bool isActive)
	{
		var store =
			new Store(name, description, phoneNumber,
			address, culture, logoUrl, isActive)
			{
				Name = name.Fix() ?? "",
				Description = description.Fix() ?? "",
				PhoneNumber = phoneNumber.Fix() ?? "",
				Address = address.Fix() ?? "",
				Culture = culture.Fix() ?? "",
				LogoUrl = logoUrl.Fix() ?? ""
			};

		return store;
	}

	public void Update
		(string name, string? description, string phoneNumber,
		string address, string? culture, string? logoUrl, bool isActive)
	{
		Name = name.Fix() ?? "";
		Description = description.Fix() ?? "";
		PhoneNumber = phoneNumber.Fix() ?? "";
		Address = address.Fix() ?? "";
		Culture = culture.Fix() ?? "";
		LogoUrl = logoUrl.Fix() ?? "";
		IsActive = isActive;

		SetUpdateDateTime();
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

	public void SetTenant(Guid tenantId)
	{
		TenantId = tenantId;
	}

	public Guid GetTenant()
	{
		return TenantId;
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
	#endregion
}