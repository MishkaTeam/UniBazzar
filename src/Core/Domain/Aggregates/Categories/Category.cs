using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;

namespace Domain.Aggregates.Categories;

public class Category : Entity
{
	public Category()
	{
		// FOR EF!
	}


	public string Name { get; private set; }
	public string IconClass { get; private set; }
    public string Slug { get; private set; }
	public Guid? ParentId { get; private set; }
	public virtual Category? Parent { get; private set; }


    public static Category Create
        (string name, Guid? parentId, string iconClass, string? slug = null)
    {
        slug ??= name.GenerateSlug();
        var category = new Category(name, parentId, iconClass, slug)
        {
            Name = name.Fix() ?? "",
            ParentId = ValidateParentId(parentId),
            IconClass = iconClass.Fix() ?? "",
            Slug = slug
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

    private Category
        (string name, Guid? parentId, string iconClass, string slug)
    {
        Name = name;
        ParentId = parentId;
        IconClass = iconClass;
        Slug = slug;
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