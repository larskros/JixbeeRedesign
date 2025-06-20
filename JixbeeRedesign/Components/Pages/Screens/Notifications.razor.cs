﻿using JixbeeRedesign.Models;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Notifications
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public NotificationService NotificationService { get; set; }

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
