using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Notifications
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private List<Notification> allNotifications = new List<Notification>();

       

        public class Notification
        {
            public string? Title { get; set; }
            public DateTime? Date { get; set; }
            public bool Unread { get; set; }
        }

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadNotifications();
        }

        private async Task LoadNotifications()
        {
            allNotifications = new List<Notification>
            {
                new Notification
                {
                    Title = "Je volgende uitbetaling staat klaar",
                    Date = new DateTime(2025, 06, 11),
                    Unread = true
                },
                new Notification
                {
                    Title = "Wijziging algemene voorwaarden",
                    Date = new DateTime(2025, 06, 05),
                    Unread = false
                },
                new Notification
                {
                    Title = "Nooit meer zonder saldo? Check nu snel hoe je dit kunt voorkomen!",
                    Date = new DateTime(2025, 05, 21),
                    Unread = false
                }
            };
        }
    }
}
