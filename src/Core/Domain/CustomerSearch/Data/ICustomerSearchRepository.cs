namespace Domain.CustomerSearch.Data;

public interface ICustomerSearchRepository
{
    Task<SuggestionItem> SuggestAsync(string mobile);
}