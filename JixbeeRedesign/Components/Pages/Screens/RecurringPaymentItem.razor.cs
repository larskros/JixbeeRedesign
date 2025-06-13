using JixbeeRedesign.Models;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class RecurringPaymentItem
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public RecurringPaymentService RecurringPaymentService { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Parameter] public int RecurringPaymentId { get; set; }

        private RecurringPayment? recurringPayment;

        private EditForm editForm;
        private int[] days = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };
        private string? RecurringPaymentTitleClass { get; set; }
        private bool hasEndDate { get; set; }
        private bool isDisabled { get; set; }

        private int activeIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            recurringPayment = await RecurringPaymentService.GetByIdAsync(RecurringPaymentId);
        }

        private void HandleOnValidSubmit()
        {

        }
        private void HandleTitleInput(string value)
        {
            recurringPayment.Title = value;
            if (string.IsNullOrEmpty(value))
            {
                RecurringPaymentTitleClass = "none";
            }
            else
            {
                RecurringPaymentTitleClass = "filled-in-title";
            }
            CheckFields();
            StateHasChanged();
        }

        private void CheckFields()
        {
            if (string.IsNullOrWhiteSpace(recurringPayment.Title) || recurringPayment.Amount == 0 || recurringPayment.Amount == null)
            {
                isDisabled = true;
            }
            else
            {
                isDisabled = false;
            }
        }
    }
}
