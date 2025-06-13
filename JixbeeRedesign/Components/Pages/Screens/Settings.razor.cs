using JixbeeRedesign.Models;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Settings
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private string preferredLanguage { get; set; }
        private string[] languages = { "nl", "en"};

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            preferredLanguage = "nl";
        }
    }
}
