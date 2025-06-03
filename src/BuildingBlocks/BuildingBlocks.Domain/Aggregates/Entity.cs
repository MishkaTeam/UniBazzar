using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Domain.SeedWork;

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

	public Guid InsertedBy { get; protected set; }
	public Guid UpdatedBy { get; protected set; }

	public long InsertDateTime { get; protected set; }
	public long UpdateDateTime { get; protected set; }

	public Guid GetOwner()
	{
		return OwnerId;
	}

	public Guid GetStore()
	{
		return StoreId;
	}
	
	public void SetStore(Guid storeId)
	{
		StoreId = storeId;
	}
	
	public void SetOwner(Guid ownerId)
	{
		OwnerId = ownerId;
	}
	
	public void SetInsertBy(Guid Id)
	{
		InsertedBy = Id;
	}

	public void IncreaseVersion()
	{
		Version++;
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
	public void SetVersionAndIncrease(int oldVersion)
	{
		Version = oldVersion;
		IncreaseVersion();
	}


}