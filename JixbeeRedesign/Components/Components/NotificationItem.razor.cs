using JixbeeRedesign.Components.Pages.Screens;
using JixbeeRedesign.Models;
using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Components
{
    public partial class NotificationItem
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Parameter] public int NotificationId { get; set; }

        private Notification? notification;

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadNotificatios();
        }

        private async Task LoadNotificatios()
        {
            notification = await NotificationService.GetByIdAsync(NotificationId);
        }

    }
}
