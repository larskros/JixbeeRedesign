using Microsoft.AspNetCore.Components;
using static MudBlazor.Colors;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class PayoutSuccess
    {
        [Parameter] public string? Class { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public int InitialIndex { get; set; }

        private string ConfettiColor { get; set; } = "var(--primarycolor100)";


        private int activeIndex = 0;

        private record ConfettiData(string Color, int Rotation, double Top, double Left, string Delay);

        private List<ConfettiData> ConfettiList = new();

        protected override void OnInitialized()
        {
            var rand = new Random();
            for (int i = 0; i < 30; i++)
            {
                ConfettiList.Add(new ConfettiData(
                    Color: i % 2 == 0 ? "var(--primarycolor100)" : "var(--secondarycolor100)", // blue/yellow
                    Rotation: rand.Next(0, 360),
                    Top: Math.Round(60 + rand.NextDouble() * 40, 0),
					Left: Math.Round(rand.NextDouble() * 100, 0),
                    Delay: 0 + "." + rand.Next(10).ToString()
				));
            }
        }

        private async Task OnActiveIndexChanged(int index)
        {
            activeIndex = index;
            await ActiveIndexChanged.InvokeAsync(index);
            StateHasChanged();
        }

        private void HandleComplete()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
