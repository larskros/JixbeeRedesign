using JixbeeRedesign.Components.Pages.Screens;
using JixbeeRedesign.Models;
using static MudBlazor.CategoryTypes;

namespace JixbeeRedesign.Services
{
    public class RecurringPaymentService
    {
        private readonly List<RecurringPayment> _recurringPayments = new()
        {
            new RecurringPayment { Id = 0, Title = "Salaris", DayOfTheMonth = 23, RemainingPayments = 16, Amount = 650 },
            new RecurringPayment { Id = 1, Title = "Koffie budget", DayOfTheMonth = 28, RemainingPayments = 1, Amount = 40 },
            new RecurringPayment { Id = 2, Title = "Telefoon abonnement", DayOfTheMonth = 9, RemainingPayments = 4, Amount = 15 },
        };

        public Task<List<RecurringPayment>> GetAllAsync() => Task.FromResult(_recurringPayments);

        public Task<RecurringPayment?> GetByIdAsync(int id) =>
            Task.FromResult(_recurringPayments.FirstOrDefault(n => n.Id == id));

        public Task<RecurringPayment?> UpdateByIdAsync(int id, RecurringPayment updatedPayment)
        {
            var current = _recurringPayments.FirstOrDefault(p => p.Id == id);
            if (current is null)
                return Task.FromResult<RecurringPayment?>(null);

            current.Title = updatedPayment.Title;
            current.DayOfTheMonth = updatedPayment.DayOfTheMonth;
            current.RemainingPayments = updatedPayment.RemainingPayments;
            current.Amount = updatedPayment.Amount;
            current.EndDate = updatedPayment.EndDate;

            return Task.FromResult<RecurringPayment?>(current);
        }
            
    }
}
