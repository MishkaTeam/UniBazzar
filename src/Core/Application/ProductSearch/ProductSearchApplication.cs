
using Domain.ProductSearch;
using Domain.ProductSearch.Data;

namespace Application.ProductSearch;

public class ProductSearchApplication(IProductSearchRepository productSearchRepository)
{
    public Task<List<SuggestionItem>> SuggestAsync(string searchText)
    {
        return productSearchRepository.SuggestAsync(searchText);
    }
}
