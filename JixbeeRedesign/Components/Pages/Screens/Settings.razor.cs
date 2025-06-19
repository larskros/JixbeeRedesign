using JixbeeRedesign.Models;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Settings
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        [Inject] protected ThemeStateService ThemeState { get; set; }

        private string preferredLanguage { get; set; }

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(ThemeState.PreferredTheme))
            {
                ThemeState.PreferredTheme = "light";
                Console.WriteLine("onload 2 : " + ThemeState.PreferredTheme);
            }
            else
            {
                Console.WriteLine("onload: " + ThemeState.PreferredTheme);
            }
            preferredLanguage = "nl";
        }


        private async void OnThemeChanged(string theme)
        {
            ThemeState.PreferredTheme = theme;
            Console.WriteLine("chosen theme: " + ThemeState.PreferredTheme);
            await JS.InvokeVoidAsync("setTheme", ThemeState.PreferredTheme);
        }
    }
}
