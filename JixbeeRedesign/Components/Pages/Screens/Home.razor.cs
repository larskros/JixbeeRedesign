using JixbeeRedesign.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Home
    {
        [Inject] protected WithdrawStateService WithdrawState { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Parameter] public EventCallback<int> WithdrawAmountChanged { get; set; }
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
        private int WithdrawAmount { get; set; }
        private bool isDisabled { get; set; }


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

        private void WithdrawMoney(int value)
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

        private async Task OnWithdrawInputChanged(decimal value)
        {
            WithdrawAmount = (int)value;
            if (WithdrawAmount > 0)
            {
                isDisabled = false;
            }
            else
            {
                isDisabled = true;
            }
            await WithdrawAmountChanged.InvokeAsync((int)value);
            StateHasChanged();

        }
    }
}
