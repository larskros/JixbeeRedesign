using MudBlazor;

namespace JixbeeRedesign.Models
{
    public class RecurringPayment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int DayOfTheMonth { get; set; }
        public int RemainingPayments { get; set; }
        public double Amount { get; set; }
        public SwipeDirection? SwipedDirection { get; set; } = null;
        public bool JustSwiped { get; set; } = false;
    }
}
