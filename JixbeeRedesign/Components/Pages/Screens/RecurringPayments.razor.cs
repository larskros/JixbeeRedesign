using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class RecurringPayments
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private List<RecurringPayment> allRecurringPayments = new List<RecurringPayment>();

        private bool showPopup { get; set; }
        private int popupHeight { get; set; } = 0;
        private string popupBgColor { get; set; } = "rgba(31, 31, 31, 0)";
        private string visible { get; set; } = "hidden";
        private string popupButtonText { get; set; } = "Inplannen";
        private string? recurringPaymentAmount { get; set; }
        private int[] days = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };

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

        private void HandleNewRecurringPayment()
        {
            showPopup = true;
            popupHeight = 90;
            popupBgColor = "rgba(31, 31, 31, 0.5)";
            visible = "visible";
            StateHasChanged();
        }
        private async void ClosePopup()
        {
            showPopup = false;
            popupHeight = 0;
            popupBgColor = "rgba(31, 31, 31, 0)";
            await Task.Delay(350);
            visible = "hidden";
            StateHasChanged();
        }
    }
}
