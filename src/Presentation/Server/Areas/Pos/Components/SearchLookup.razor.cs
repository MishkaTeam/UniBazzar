using System.Text.RegularExpressions;
using Domain.ProductSearch;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Server.Areas.Pos.Components
{
    public partial class SearchLookup : IDisposable
    {
        private string searchText = string.Empty;
        private bool showSuggestions = false;
        private bool isLoading = false;
        private int selectedIndex = -1;
        private List<SuggestionItem> suggestions = new();
        private CancellationTokenSource? debounceCts;
        private bool _disposed;


        private async Task OnInputChanged(ChangeEventArgs e)
        {
            searchText = e.Value?.ToString() ?? "";

            if (debounceCts is not null)
            {
                debounceCts.Cancel();
                debounceCts.Dispose();
            }

            debounceCts = new CancellationTokenSource();
            var token = debounceCts.Token;

            await Task.Delay(300); // Debounce time
            if (token.IsCancellationRequested)
                return;

            await LoadSuggestions();
        }
        private void ShowSuggestions()
        {
            showSuggestions = true;
            selectedIndex = -1;
        }

        private async Task LoadSuggestions()
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                suggestions.Clear();
                showSuggestions = false;
                return;
            }

            try
            {
                
                var result = await productApplication.SuggestAsync(searchText);
                if (_disposed) return; // 🔥 خیلی مهم!!

                suggestions = result ?? new();
                showSuggestions = true;
            }
            catch (Exception ex)
            {
                // Log or handle error
            }
        }


        private async Task HandleKeyDown(KeyboardEventArgs e)
        {
            if (!showSuggestions || isLoading)
                return;

            if (e.Key == "ArrowDown")
            {
                selectedIndex = (selectedIndex + 1) % suggestions.Count;
            }
            else if (e.Key == "ArrowUp")
            {
                selectedIndex = (selectedIndex - 1 + suggestions.Count) % suggestions.Count;
            }
            else if (e.Key == "Enter" && selectedIndex >= 0)
            {
                SelectSuggestion(selectedIndex);
            }

            await InvokeAsync(StateHasChanged);
        }

        private void SelectSuggestion(int index)
        {
            searchText = suggestions[index].ProductTitle;
            showSuggestions = false;
        }

        private string Highlight(string text)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return text;

            var regex = new Regex(Regex.Escape(searchText), RegexOptions.IgnoreCase);
            return regex.Replace(text, match => $"<mark>{match.Value}</mark>");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("addOutsideClickListener", DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable]
        public void HideSuggestions()
        {
            showSuggestions = false;
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _disposed = true;
        }


         
    }
}