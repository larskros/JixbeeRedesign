using JixbeeRedesign.Components.Pages.Screens;
using JixbeeRedesign.Models;

namespace JixbeeRedesign.Services
{
    public class NotificationService
    {
        private readonly List<Notification> _notifications = new()
        {
            new Notification { Id = 0, Title = "Je volgende uitbetaling staat klaar", Date = new DateTime(2025, 06, 11), Unread = true, Author = "Jixbee", Message = "Goed nieuws! Je volgende uitbetaling is zojuist verwerkt en staat binnen 1-2 werkdagen op je rekening." },
            new Notification { Id = 1, Title = "Wijziging algemene voorwaarden", Date = new DateTime(2025, 06, 05), Unread = false, Author = "Jixbee", Message = "Per 1 juli 2025 passen wij onze algemene voorwaarden aan. Deze wijzigingen zijn nodig om beter aan te sluiten op de huidige wet- en regelgeving én om onze dienstverlening verder te verbeteren. Het is belangrijk dat je op de hoogte bent van deze aanpassingen. Je kunt de nieuwe voorwaarden nu al bekijken via onze website. Door onze diensten te blijven gebruiken na deze datum, ga je automatisch akkoord met de nieuwe voorwaarden." },
            new Notification { Id = 2, Title = "Nooit meer zonder saldo? Check nu snel hoe je dit kunt voorkomen!", Date = new DateTime(2025, 05, 21), Unread = false, Author = "Jixbee", Message = "Wist je dat je automatische meldingen kunt instellen om een laag saldo te voorkomen? Ontdek nu hoe je dit eenvoudig activeert." }
        };

        public Task<List<Notification>> GetAllAsync() => Task.FromResult(_notifications);

        public Task<Notification?> GetByIdAsync(int id) =>
            Task.FromResult(_notifications.FirstOrDefault(n => n.Id == id));

    }
}
