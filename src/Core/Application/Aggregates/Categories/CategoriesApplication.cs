using Application.Aggregates.Categories.ViewModels;
using Domain;
using Domain.Aggregates.Categories;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Categories;

public class CategoriesApplication
	(ICategoryRepository categoryRepository,
	IUnitOfWork unitOfWork)
{
	public async Task<CategoryViewModel> CreateCategoryAsync(CreateCategoryViewModel viewModel)
	{
		var category = Category.Create
			(viewModel.Name, viewModel.ParentId, viewModel.IconClass);

		await categoryRepository.AddCategoryAsync(category);
		await unitOfWork.CommitAsync();

		return category.Adapt<CategoryViewModel>();
	}

	public async Task<List<CategoryViewModel>> GetCategoriesAsync()
	{
		var categories =
			await categoryRepository.GetAllCategoriesAsync();

		return categories.Adapt<List<CategoryViewModel>>();
	}

	public async Task<CategoryViewModel> GetCategoryAsync(Guid id)
	{
		var category =
			await categoryRepository.GetCategoryAsync(id);

		return category.Adapt<CategoryViewModel>();
	}

	public async Task<List<CategoryViewModel>> GetSubCategoriesAsync(Guid parentId)
	{
		var subCategories =
			await categoryRepository.GetSubCategoriesAsync(parentId: parentId);

		return subCategories.Adapt<List<CategoryViewModel>>();
	}

	public async Task DeleteCategoryAsync(Guid id)
	{
		var categoryForDelete =
			await categoryRepository.GetCategoryAsync(id);

		if (categoryForDelete == null || categoryForDelete.Id == Guid.Empty)
		{
			throw new Exception(Errors.NotFound);
		}

		categoryRepository.RemoveCategory(categoryForDelete);
		await unitOfWork.CommitAsync();
	}
}