using Microsoft.AspNetCore.Components;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Hours
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private List<string> SegmentItems = new()
        {
            "Week", "Maand", "Jaar"
        };

        private int activeIndex = 0;

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
