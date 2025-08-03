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

        await categoryRepository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();

        return category.Adapt<CategoryViewModel>();
    }

    public async Task<CategoryViewModel> UpdateCategoryAsync(CategoryViewModel updateViewModel)
    {
        var categoryForUpdate =
            await categoryRepository.GetByIdAsync(updateViewModel.Id);

        if (categoryForUpdate == null || categoryForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.Category);

            throw new Exception(message);
        }

        categoryForUpdate.Update(updateViewModel.Name,
            updateViewModel.ParentId, updateViewModel.IconClass);

        await unitOfWork.SaveChangesAsync();
        return categoryForUpdate.Adapt<CategoryViewModel>();
    }

    public async Task<List<CategoryViewModel>> GetCategoriesAsync()
    {
        var categories =
            await categoryRepository.GetAllWithIncludeAsync();

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
            await categoryRepository.GetByIdAsync(id);

        return category?.Adapt<CategoryViewModel>();
    }

    public async Task<List<CategoryViewModel>> GetSubCategoriesAsync(Guid parentId)
    {
        var subCategories =
            await categoryRepository.GetSubCategoriesAsync(parentId);

        var subCategoryViewModels =
            subCategories.Adapt<List<CategoryViewModel>>();

        subCategoryViewModels =
            await SetSubCategoriesCount(subCategoryViewModels);

        return subCategoryViewModels;
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var categoryForDelete =
            await categoryRepository.GetByIdAsync(id);

        if (categoryForDelete == null || categoryForDelete.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.Category);

            throw new Exception(message);
        }

        await categoryRepository.RemoveAsync(categoryForDelete);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<List<MenuCategoryViewModel>> GetMenuCategoriesAsync()
    {
        var menuCategories = await categoryRepository.GetCurrentStoreCategoriesAsync();

        var rootCategories = menuCategories.Where(c => c.ParentId == null).ToList();

        return rootCategories.Select(c => MapCategoryToViewModel(c, menuCategories)).ToList();
    }


    private MenuCategoryViewModel MapCategoryToViewModel(Category category, List<Category> allCategories, int currentLevel = 1)
    {
        if (category == null || currentLevel > 3) // توقف در سطح سوم
            return null;

        var viewModel = new MenuCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            ChildCategories = new List<MenuCategoryViewModel>()
        };

        // پیدا کردن فرزندان این دسته‌بندی از لیست کل دسته‌بندی‌ها
        var children = allCategories
            .Where(c => c.ParentId == category.Id)
            .Select(c => MapCategoryToViewModel(c, allCategories, currentLevel + 1))
            .Where(vm => vm != null)
            .ToList();

        viewModel.ChildCategories.AddRange(children);

        return viewModel;
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