using Domain.ProductSearch;
using Domain.ProductSearch.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.ProductSearch;

public class ProductSearchRepository
    (UniBazzarContext context, IExecutionContextAccessor executionContextAccessor) : IProductSearchRepository
{
    public async Task<List<SuggestionItem>> SuggestAsync(string searchText)
    {
        var query = await context.Products
                           //.Include(x => x.Category)
                           .Select(x => new SuggestionItem
                           {
                               //Category = x.Category.Name,
                               IsCategory = false, // 
                               ProductId = x.Id,
                               ProductTitle = x.Name,
                               StoreId = x.StoreId,
                           })
                           .Where(x => x.ProductTitle.Contains(searchText))
                           .Where(x => x.StoreId == executionContextAccessor.StoreId).ToListAsync();

        return query;
    }
}
