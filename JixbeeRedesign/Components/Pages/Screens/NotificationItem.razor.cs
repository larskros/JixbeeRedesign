using JixbeeRedesign.Models;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class NotificationItem
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public NotificationService NotificationService { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Parameter] public int NotificationId { get; set; }

        private Notification? notification;

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            notification = await NotificationService.GetByIdAsync(NotificationId);
        }
    }
}
