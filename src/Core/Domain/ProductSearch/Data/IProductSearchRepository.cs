
namespace Domain.ProductSearch.Data;

public interface IProductSearchRepository
{
    Task<List<SuggestionItem>> SuggestAsync(string searchText);
}
