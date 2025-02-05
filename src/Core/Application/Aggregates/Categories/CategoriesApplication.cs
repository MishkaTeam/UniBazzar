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

	public async Task<CategoryViewModel> UpdateCategoryAsync(CategoryViewModel updateViewModel)
	{
		var categoryForUpdate =
			await categoryRepository.GetCategoryAsync(updateViewModel.Id);

		if (categoryForUpdate == null || categoryForUpdate.Id == Guid.Empty)
		{
			var message =
				string.Format(Errors.NotFound, Resources.DataDictionary.Category);

			throw new Exception(message);
		}

		categoryForUpdate.Update(updateViewModel.Name,
			updateViewModel.ParentId, updateViewModel.IconClass);

		await unitOfWork.CommitAsync();
		return categoryForUpdate.Adapt<CategoryViewModel>();
	}

	public async Task<List<CategoryViewModel>> GetCategoriesAsync()
	{
		var categories =
			await categoryRepository.GetAllCategoriesAsync();

		return categories.Adapt<List<CategoryViewModel>>();
	}

	public async Task<List<CategoryViewModel>> GetRootCategoriesAsync()
	{
		var categories =
			await categoryRepository.GetRootCategoriesAsync();

		var categoryViewModels =
			categories.Adapt<List<CategoryViewModel>>();

		categoryViewModels =
			await SetSubCategoriesCount(categoryViewModels);

		return categoryViewModels;
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

		var subCategoryViewModels =
			subCategories.Adapt<List<CategoryViewModel>>();

		subCategoryViewModels =
			await SetSubCategoriesCount(subCategoryViewModels);

		return subCategoryViewModels;
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


	private async Task<List<CategoryViewModel>> SetSubCategoriesCount(List<CategoryViewModel> categories)
	{
		foreach (var category in categories)
		{
			category.SubCategoriesCount =
				await categoryRepository.GetSubCategoriesCountAsync(category.Id);
		}

		return categories;
	}
}