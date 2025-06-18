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

        private string preferredLanguage { get; set; }
        private string[] languages = { "nl", "en"};
        private string preferredTheme { get; set; }
        private string[] themes = { "light", "dark", "creme", "gold"};

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            preferredLanguage = "nl";
        }


        private async void OnThemeChanged(string theme)
        {
            preferredTheme = theme;
            Console.WriteLine("chosen theme: " + preferredTheme);
            await JS.InvokeVoidAsync("setTheme", preferredTheme);
        }
    }
}
