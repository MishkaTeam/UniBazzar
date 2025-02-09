using BuildingBlocks.Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingBlocks.Domain.Aggregates;

public class Entity : IEntity
{
	public Entity()
	{
		Id = Guid.NewGuid();

		SetInsertDateTime();
	}

	[DatabaseGenerated
	(DatabaseGeneratedOption.None)]
	public Guid Id { get; protected set; }

	public int Ordering { get; protected set; }
	public int Version { get; protected set; }

	public Guid StoreId { get; protected set; }
	public Guid OwnerId { get; protected set; }
	public Guid TenantId { get; protected set; }

	public Guid InsertedBy { get; protected set; }
	public Guid UpdatedBy { get; protected set; }

	public long InsertDateTime { get; protected set; }
	public long UpdateDateTime { get; protected set; }

	public Guid GetOwner()
	{
		return OwnerId;
	}

	public Guid GetTenant()
	{
		return TenantId;
	}

	public void SetOwner(Guid ownerId)
	{
		OwnerId = ownerId;
	}

	public void SetTenant(Guid tenantId)
	{
		TenantId = tenantId;
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

}