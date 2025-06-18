using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.Globalization;
using static JixbeeRedesign.Components.Pages.Screens.RecurringPayments;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Home : ComponentBase, IAsyncDisposable
    {
        [Inject] protected WithdrawStateService WithdrawState { get; set; }
        [Inject] private IJSRuntime JS { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Parameter] public EventCallback<decimal?> WithdrawAmountChanged { get; set; }
        [Parameter] public int WithdrawMoneyAmount { get; set; }

        private List<string> SegmentItems = new()
        {
            "Salaris", "Vakantiegeld", "Overuren", "Declaraties"
        };

        private int activeIndex = 0;

        private double alreadyWithdrawn { get; set; }
        private bool showPopup { get; set; }
        private int popupHeight { get; set; } = 0;
        private string popupTitle { get; set; }
        private string popupBgColor { get; set; } = "rgba(31, 31, 31, 0)";
        private string visible { get; set; } = "hidden";
        private string popupButtonText { get; set; } = "Opnemen";
        private int MaximumSalary { get; set; } = 1250;
        private decimal? WithdrawAmount { get; set; }
        private bool isDisabled { get; set; }

        private string? CurrentFormat = "N2";
        private ElementReference _inputContainer;
        private MudNumericField<decimal?>? _withdrawFieldRef;
        private DotNetObjectReference<Home>? _dotNetRef;


        private EditForm editForm;
        private Withdraw? model = new Withdraw();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetRef = DotNetObjectReference.Create(this);

                await JS.InvokeVoidAsync("numericInputHelper.setupFocusListeners", _inputContainer, _dotNetRef);
                await JS.InvokeVoidAsync("numericInputHelper.setupNumericInputFilter", _inputContainer);
            }
        }

        protected override void OnInitialized()
        {
            switch (activeIndex)
            {
                case 0:
                    popupTitle = "Salaris";
                    break;
                case 1:
                    popupTitle = "Vakantiegeld";
                    break;
                case 2:
                    popupTitle = "Overuren";
                    break;
                case 3:
                    popupTitle = "Declaraties";
                    break;
                default:
                    popupTitle = "Salaris";
                    break;
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
            _dotNetRef?.Dispose();
        }

        private class Withdraw
        {
            public int WithdrawAmount { get; set; }
        }

        private async Task OnActiveIndexChanged(int index)
        {
            activeIndex = index;
            switch (activeIndex) {
                case 0:
                    popupTitle = "Salaris";
                    break;
                case 1:
                    popupTitle = "Vakantiegeld";
                    break;
                case 2:
                    popupTitle = "Overuren";
                    break;
                case 3:
                    popupTitle = "Declaraties";
                    break;
                default:
                    popupTitle = "Salaris";
                    break;
            }
            await ActiveIndexChanged.InvokeAsync(index);
            StateHasChanged();
        }

        private void HandleWithdraw()
        {
            showPopup = true;
            popupHeight = 90;
            isDisabled = true;
            popupBgColor = "rgba(31, 31, 31, 0.5)";
			visible = "visible";
            StateHasChanged();
		}

        private void SetMaximumAvailableSalary()
        {
            WithdrawAmount = MaximumSalary;
        }

        private void WithdrawMoney(decimal? value)
        {
            WithdrawState.WithdrawAmount = value;
            NavigationManager.NavigateTo("/uitbetaling-succesvol");
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

        private async Task OnWithdrawInputChanged(decimal? value)
        {
            WithdrawAmount = value;
            if (WithdrawAmount > 0)
            {
                isDisabled = false;
            }
            else
            {
                isDisabled = true;
            }
            await WithdrawAmountChanged.InvokeAsync(value);
            StateHasChanged();

        }
        private void CheckFields()
        {
            if (WithdrawAmount == 0 || WithdrawAmount == null)
            {
                WithdrawAmount = null;
                isDisabled = true;
            }
            else
            {
                isDisabled = false;
            }
        }

        private void HandleOnValidSubmit()
        {

        }
    }
}
