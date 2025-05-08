using Domain.CustomerSearch;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Server.Areas.Pos.Components;

public partial class SearchCustomer
{
    private string searchMobile = string.Empty;
    private bool showSuggestions = false;

    private SuggestionItem suggestion = new();



    private async Task OnInputChanged(ChangeEventArgs e)
    {
        searchMobile = e.Value?.ToString() ?? "";

        if (searchMobile.Length == Constants.FixedLength.CellPhoneNumberIran)
        {
            suggestion = 
                await customerApplication.SuggestAsync(searchMobile);

            if (suggestion is not null)
            {
                searchMobile = string.Empty;
            }

            return;
        }

        suggestion = null;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("addOutsideClickListenerCustomerSearch", DotNetObjectReference.Create(this));
        }
    }

    private void ShowSuggestions()
    {
        showSuggestions = true;
    }

    [JSInvokable]
    public void HideSuggestions()
    {
        showSuggestions = false;
        InvokeAsync(StateHasChanged);
    }

}