using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;

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
        HttpClient httpClient = new HttpClient();

        protected override void OnInitialized()
        {
            httpClient.BaseAddress = new Uri("https://localhost:7078");
        }
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
                var result = await httpClient.GetFromJsonAsync<List<SuggestionItem>>($"/api/Search?q={searchText}");

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
            searchText = suggestions[index].Title;
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

        public class SuggestionItem
        {
            public string Title { get; set; } = "";
            public string? Category { get; set; }
            public bool IsCategory { get; set; } = false;
        }


         
    }
}