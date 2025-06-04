using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class RecurringPayments
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private List<RecurringPayment> allRecurringPayments = new List<RecurringPayment>();

        public class RecurringPayment
        {
            public string? Title { get; set; }
            public int DayOfTheMonth { get; set; }
            public int RemainingPayments { get; set; }
            public int Amount { get; set; }
        }

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadRecurringPayments();
        }

        private async Task LoadRecurringPayments()
        {
            allRecurringPayments = new List<RecurringPayment>
            {
                new RecurringPayment
                {
                    Title = "Salaris",
                    DayOfTheMonth = 23,
                    RemainingPayments = 16,
                    Amount = 650
                },
                new RecurringPayment
                {
                    Title = "Koffie budget",
                    DayOfTheMonth = 28,
                    RemainingPayments = 1,
                    Amount = 40
                },
                new RecurringPayment
                {
                    Title = "Telefoon abonnement",
                    DayOfTheMonth = 9,
                    RemainingPayments = 4,
                    Amount = 15
                }
            };
        }

        private async Task OnActiveIndexChanged(int index)
        {
            activeIndex = index;
            await ActiveIndexChanged.InvokeAsync(index);
            StateHasChanged();
        }

        private void HandleWithdraw()
        {

        }
    }
}
