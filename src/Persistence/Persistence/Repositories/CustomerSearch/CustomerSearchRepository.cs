using Domain.CustomerSearch;
using Domain.CustomerSearch.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.CustomerSearch;

public class CustomerSearchRepository
    (UniBazzarContext context, IExecutionContextAccessor executionContextAccessor) : ICustomerSearchRepository
{
    public async Task<SuggestionItem> SuggestAsync(string mobile)
    {
        var query = await context.Customers
                            .Select(x => new SuggestionItem
                            {
                                CustomerId = x.Id,
                                LastName = x.LastName,
                                Mobile = x.Mobile,
                                NationalCode = x.NationalCode,
                                StoreId = x.StoreId,
                            })
                            .FirstOrDefaultAsync(x => x.Mobile == mobile && 
                            x.StoreId == executionContextAccessor.StoreId);

        return query;
    }
}