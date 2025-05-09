using Domain.CustomerSearch;
using Domain.CustomerSearch.Data;

namespace Application.CustomerSearch;

public class CustomerSearchApplication(ICustomerSearchRepository customerSearchRepository)
{
    public Task<SuggestionItem> SuggestAsync(string mobile)
    {
        return customerSearchRepository.SuggestAsync(mobile);
    }
}