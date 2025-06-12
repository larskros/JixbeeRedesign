using JixbeeRedesign.Models;
using Microsoft.AspNetCore.Components;
using static JixbeeRedesign.Components.Components.NotificationItem;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Notifications
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private List<Notification> allNotifications = new();

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            allNotifications = await NotificationService.GetAllAsync();
        }

        private async Task LoadNotifications()
        {
            
        }

        private void OpenNotification(int id)
        {
            NavigationManager.NavigateTo($"/notificaties/{id}");
        }
    }
}
