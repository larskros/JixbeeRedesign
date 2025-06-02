using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Home
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }
        [Parameter] public EventCallback<int> WithdrawAmountChanged { get; set; }

        private List<string> SegmentItems = new()
        {
            "Salaris", "Vakantiegeld", "Overuren", "Declaraties"
        };

        private int activeIndex = 0;

        private bool showPopup { get; set; }
        private int popupHeight { get; set; } = 0;
        private string popupBgColor { get; set; } = "rgba(31, 31, 31, 0)";
        private string visible { get; set; } = "hidden";
        private string popupButtonText { get; set; } = "Opnemen";
        private int MaximumSalary { get; set; } = 1250;
        private int WithdrawAmount { get; set; }



        private async Task OnActiveIndexChanged(int index)
        {
            activeIndex = index;
            await ActiveIndexChanged.InvokeAsync(index);
            StateHasChanged();
        }

        private void HandleWithdraw()
        {
            showPopup = true;
            popupHeight = 90;
            popupBgColor = "rgba(31, 31, 31, 0.5)";
			visible = "visible";
			StateHasChanged();
		}

        private void SetMaximumAvailableSalary()
        {

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

        private async Task OnWithdrawInputChanged(int index)
        {
            WithdrawAmount = index;
            await WithdrawAmountChanged.InvokeAsync(index);
            StateHasChanged();

        }
    }
}
