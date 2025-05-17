using System.Threading.Tasks;
using Domain.CustomerSearch;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Server.Areas.Pos.Components;

public partial class SearchCustomer
{

    [Parameter]
    public EventCallback<string> OnCreateCustomer { get; set; }

    private string searchMobile = string.Empty;
    private bool isFocus = false;
    private bool isLoading = false;

    private SuggestionItem suggestion = new();


    private async Task OnInputChanged(ChangeEventArgs e)
    {
        searchMobile = e.Value?.ToString() ?? "";

        if (searchMobile.Length != Constants.FixedLength.CellPhoneNumberIran)
        {
            suggestion = null;

            return;
        }

        isLoading = true;

        // Test for loading section
        //await Task.Delay(2000);

        suggestion =
            await customerApplication.SuggestAsync(searchMobile);

        if (suggestion is not null)
        {
            searchMobile = string.Empty;
        }

        isLoading = false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("addOutsideClickListenerCustomerSearch", DotNetObjectReference.Create(this));
        }
    }

    private void OnFocus()
    {
        isFocus = true;
    }

    [JSInvokable]
    public void LeaveFocus()
    {
        isFocus = false;
        InvokeAsync(StateHasChanged);
    }

    private async Task CreateCustomerClick()
    {
        await OnCreateCustomer.InvokeAsync(searchMobile);

        searchMobile = string.Empty;
    }

    public async Task SetCustomerAsync(string? mobile = null)
    {
        await OnInputChanged(new ChangeEventArgs()
        {
            Value = mobile
        });
    }

}