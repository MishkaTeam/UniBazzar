using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;

namespace Domain.Aggregates.Categories;

public class Category : Entity
{
	public Category()
	{
		// FOR EF!
	}


	public static Category Create
		(string name, Guid? parentId, string iconClass)
	{
		var category = new Category(name, parentId, iconClass)
		{
			Name = name.Fix() ?? "",
			ParentId = ValidateParentId(parentId),
			IconClass = iconClass.Fix() ?? "",
		};

		return category;
	}

	public void Update
		(string name, Guid? parentId, string iconClass)
	{
		Name = name.Fix() ?? "";
		ParentId = ValidateParentId(parentId);
		IconClass = iconClass.Fix() ?? "";

		SetUpdateDateTime();
	}


	public string Name { get; private set; }
	public string IconClass { get; private set; }

	public Guid? ParentId { get; private set; }
	public virtual Category? Parent { get; private set; }

	private Category
		(string name, Guid? parentId, string iconClass)
	{
		Name = name;
		ParentId = parentId;
		IconClass = iconClass;
	}

	private static Guid? ValidateParentId(Guid? parentId)
	{
		if (parentId == Guid.Empty)
		{
			parentId = null;
		}

		return parentId;
	}
}