using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class PayoutHistory
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private List<Payout> allPayouts = new List<Payout>();

        private bool showPopup { get; set; }
        private int popupHeight { get; set; } = 0;
        private string popupBgColor { get; set; } = "rgba(31, 31, 31, 0)";
        private string visible { get; set; } = "hidden";
        private string popupButtonText { get; set; } = "Inplannen";
        private string? recurringPaymentAmount { get; set; }

        public class Payout
        {
            public string? Title { get; set; }
            public string? Type { get; set; }
            public int Amount { get; set; }
            public DateTime? Date { get; set; }
        }

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadPayoutHistory();
        }

        private async Task LoadPayoutHistory()
        {
            allPayouts = new List<Payout>
            {
                new Payout
                {
                    Title = "Koffie budget",
                    Type = "Geplande opname",
                    Amount = 40,
                    Date = new DateTime(2025, 04, 28)
                },
                new Payout
                {
                    Title = "Salaris",
                    Type = "Geplande opname",
                    Amount = 650,
                    Date = new DateTime(2025, 04, 23)
                },
                new Payout
                {
                    Title = "Telefoon abonnement",
                    Type = "Geplande opname",
                    Amount = 25,
                    Date = new DateTime(2025, 04, 09)
                },
                new Payout
                {
                    Title = "Koffie budget",
                    Type = "Geplande opname",
                    Amount = 40,
                    Date = new DateTime(2025, 03, 28)
                },
                new Payout
                {
                    Title = "Salaris",
                    Type = "Geplande opname",
                    Amount = 650,
                    Date = new DateTime(2025, 03, 23)
                },
                new Payout
                {
                    Title = "Telefoon abonnement",
                    Type = "Geplande opname",
                    Amount = 40,
                    Date = new DateTime(2025, 03, 09)
                }
            };
        }
    }
}
