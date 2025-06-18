using JixbeeRedesign.Models;
using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class RecurringPayments : Microsoft.AspNetCore.Components.ComponentBase, IAsyncDisposable
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public RecurringPaymentService RecurringPaymentService { get; set; }
        [Inject] private IJSRuntime JS { get; set; }

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

        private string? CurrentFormat = "N2";
        private ElementReference _inputContainer2;
        private MudNumericField<double?>? _withdrawFieldRef;
        private DotNetObjectReference<RecurringPayments>? _dotNetRef2;
        private int remainingPayments { get; set; }

        public int GetRemainingPayments(DateTime startDate, DateTime endDate, int dayOfMonth)
        {
            var now = DateTime.Now;

            // If end date is in the past, no remaining payments
            if (endDate < now)
                return 0;

            // Start from next due date
            var nextPayment = new DateTime(now.Year, now.Month, 1).AddDays(dayOfMonth - 1);
            if (now.Day > dayOfMonth)
                nextPayment = nextPayment.AddMonths(1);

            int months = 0;
            while (nextPayment <= endDate)
            {
                months++;
                nextPayment = nextPayment.AddMonths(1);
            }

            return months;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetRef2 = DotNetObjectReference.Create(this);

                await JS.InvokeVoidAsync("numericInputHelper.setupFocusListeners", _inputContainer2, _dotNetRef2);
                await JS.InvokeVoidAsync("numericInputHelper.setupNumericInputFilter", _inputContainer2);
            }
        }

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

        [JSInvokable]
        public void OnInputFocus()
        {
            CurrentFormat = "G";
            InvokeAsync(StateHasChanged);
        }

        [JSInvokable]
        public void OnInputBlur()
        {
            CurrentFormat = "N2";
            InvokeAsync(StateHasChanged);
        }

        public async ValueTask DisposeAsync()
        {
            _dotNetRef2?.Dispose();
        }

        private async Task OnActiveIndexChanged(int index)
        {
            activeIndex = index;
            await ActiveIndexChanged.InvokeAsync(index);
            StateHasChanged();
        }

        private async void HandleNewRecurringPayment()
        {
            recurringPayment = new RecurringPayment();
            recurringPayment.DayOfTheMonth = 1;
            showPopup = true;
            popupHeight = 90;
            popupBgColor = "rgba(31, 31, 31, 0.5)";
            visible = "visible";
            Console.WriteLine("Day of the month: " + recurringPayment.DayOfTheMonth);

            CheckFields();
            StateHasChanged();

            await Task.Delay(50);
            _dotNetRef2 = DotNetObjectReference.Create(this);

            await JS.InvokeVoidAsync("numericInputHelper.setupFocusListeners", _inputContainer2, _dotNetRef2);
            await JS.InvokeVoidAsync("numericInputHelper.setupNumericInputFilter", _inputContainer2);

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

        public void OnDayChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out var day))
                recurringPayment.DayOfTheMonth = day;
            else
                recurringPayment.DayOfTheMonth = 1;
            CheckFields();
        }

        private void CheckFields()
        {
            bool allRequiredFieldsFilled =
                !string.IsNullOrWhiteSpace(recurringPayment.Title) &&
                recurringPayment.Amount != null &&
                recurringPayment.Amount > 0 &&
                recurringPayment.DayOfTheMonth > 0;
            if (!allRequiredFieldsFilled || hasEndDate && recurringPayment.EndDate == null)
            {
                
                isDisabled = true; //button disabled
                Console.WriteLine("disabled: enddate: " + recurringPayment.EndDate + " switch: " + hasEndDate);
            }
            else
            {
                isDisabled = false; //button enabled
                Console.WriteLine("enabled: enddate: " + recurringPayment.EndDate + " switch: " + hasEndDate);
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
            if (string.IsNullOrWhiteSpace(recurringPayment.Title) || recurringPayment.Amount == 0 || recurringPayment.Amount == null || recurringPayment.DayOfTheMonth == 0)
            {
                Console.WriteLine("edit");
                isDisabled = true;
            }
            else
            {
                Console.WriteLine("edit2");
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
            if (hasEndDate)
            {
                remainingPayments = GetRemainingPayments(DateTime.Today, recurringPayment.EndDate.Value, recurringPayment.DayOfTheMonth);
            }
            else
            {
                remainingPayments = 0;
            }
            var newPayment = new RecurringPayment
            {
                Id = allRecurringPayments.Count + 1,
                Title = item.Title,
                Amount = item.Amount,
                DayOfTheMonth = item.DayOfTheMonth,
                RemainingPayments = remainingPayments,
                EndDate = item.EndDate,
            };
            Console.WriteLine($"Submitted: id={item.Id}, title={item.Title}, amount={item.Amount}, day={item.DayOfTheMonth}, enddate={item.EndDate}");
            allRecurringPayments.Add(newPayment);
            ClosePopup();
            ClosePopupEdit();
        }
        private async void HandleOnSubmitSaveEdit(RecurringPayment item)
        {
            await RecurringPaymentService.UpdateByIdAsync(item.Id, item);
            Console.WriteLine($"Edited: id={item.Id}, title={item.Title}, amount={item.Amount}, day={item.DayOfTheMonth}, enddate={item.EndDate}");
            StateHasChanged();
            ClosePopup();
            ClosePopupEdit();
        }

        private void RemoveRecurringPayment(RecurringPayment item)
        {
            allRecurringPayments.Remove(item);
        }

        private async void EditRecurringPayment(RecurringPayment item)
        {
            if (item.JustSwiped)
                return;
            isDisabled = false;
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
