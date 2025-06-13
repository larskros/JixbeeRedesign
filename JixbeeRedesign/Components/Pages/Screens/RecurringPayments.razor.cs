using JixbeeRedesign.Models;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class RecurringPayments
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public RecurringPaymentService RecurringPaymentService { get; set; }

        private bool showPopupEdit { get; set; }
        private int popupHeightEdit { get; set; } = 0;
        private string popupBgColorEdit { get; set; } = "rgba(31, 31, 31, 0)";
        private string visibleEdit { get; set; } = "hidden";
        private string popupButtonTextEdit { get; set; } = "Aanpassen";
        private bool showPopup { get; set; }
        private int popupHeight { get; set; } = 0;
        private string popupBgColor { get; set; } = "rgba(31, 31, 31, 0)";
        private string visible { get; set; } = "hidden";
        private string popupButtonText { get; set; } = "Inplannen";
        private bool isDisabled { get; set; }

        private EditForm editForm;
        private RecurringPayment? recurringPayment;
        [Parameter] public int RecurringPaymentId { get; set; }

        private List<RecurringPayment> allRecurringPayments = new();
        private decimal? recurringPaymentAmount { get; set; }
        private int[] days = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };

        private string? recurringPaymentTitle { get; set; }
        private string? recurringPaymentDay { get; set; }
        private string? RecurringPaymentTitleClass { get; set; }
        private bool hasEndDate { get; set; }
        private SwipeDirection swipeDirection;
        private bool showDeleteButton { get; set; }

        private int activeIndex = 0;
        private RecurringPayment viewModel = new RecurringPayment();


        protected override async Task OnInitializedAsync()
        {
            recurringPayment = new RecurringPayment();
            allRecurringPayments = await RecurringPaymentService.GetAllAsync();
            //recurringPayment = await RecurringPaymentService.GetByIdAsync(RecurringPaymentId);
            if (editForm?.EditContext == null)
            {
                Console.WriteLine("??? EditContext is null???");
            }
            else
            {
                isDisabled = editForm.EditContext.Validate();
            }
        }


        private async Task OnActiveIndexChanged(int index)
        {
            activeIndex = index;
            await ActiveIndexChanged.InvokeAsync(index);
            StateHasChanged();
        }

        private void HandleNewRecurringPayment()
        {
            recurringPayment = new RecurringPayment();
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
        private async void ClosePopupEdit()
        {
            showPopupEdit = false;
            popupHeightEdit = 0;
            popupBgColorEdit = "rgba(31, 31, 31, 0)";
            await Task.Delay(350);
            visibleEdit = "hidden";
            StateHasChanged();
        }

        private void HandleTitleInput(string value)
        {
            recurringPayment.Title = value;
            if(string.IsNullOrEmpty(value))
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
            if(string.IsNullOrWhiteSpace(recurringPayment.Title) || recurringPayment.Amount == 0 || recurringPayment.Amount == null)
            {
                isDisabled = true;
            }
            else
            {
                isDisabled = false;
            }
        }
        private void HandleTitleInputEdit(string value)
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
            CheckFieldsEdit();
            StateHasChanged();
        }
        private void CheckFieldsEdit()
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

        private async void HandleSwipe(SwipeDirection direction, RecurringPayment swipedItem)
        {
            swipeDirection = direction;
            foreach (var item in allRecurringPayments)
            {
                item.SwipedDirection = (item == swipedItem) ? direction : null;
                item.JustSwiped = false;
            }
            swipedItem.JustSwiped = true;
            showDeleteButton = true;
            StateHasChanged();

            await Task.Delay(300);
            swipedItem.JustSwiped = false;
        }

        public string GetCardClass(RecurringPayment item)
        {
            var baseClass = "recurring-payment-card";
            return item.SwipedDirection switch
            {
                SwipeDirection.RightToLeft => "recurring-payment-card swiped-left",
                SwipeDirection.LeftToRight => "recurring-payment-card swiped-right",
                _ => baseClass
            };
        }

        private async void HandleOnValidSubmit(RecurringPayment item)
        {
            var newPayment = new RecurringPayment
            {
                Id = allRecurringPayments.Count + 1,
                Title = item.Title,
                Amount = item.Amount,
                DayOfTheMonth = item.DayOfTheMonth,
                RemainingPayments = new Random().Next(1,40),
            };
            Console.WriteLine($"Submitted: id={item.Id}, title={item.Title}, amount={item.Amount}, day={item.DayOfTheMonth}");
            allRecurringPayments.Add(newPayment);
            ClosePopup();
        }

        private void RemoveRecurringPayment(RecurringPayment item)
        {
            allRecurringPayments.Remove(item);
        }

        private async void EditRecurringPayment(RecurringPayment item)
        {
            if (item.JustSwiped)
                return;

            recurringPayment = await RecurringPaymentService.GetByIdAsync(item.Id);
            //NavigationManager.NavigateTo($"/opnames/gepland/{item.Id}");
            showPopupEdit = true;
            popupHeightEdit = 90;
            popupBgColorEdit = "rgba(31, 31, 31, 0.5)";
            visibleEdit = "visible";
            StateHasChanged();
        }
    }
}
