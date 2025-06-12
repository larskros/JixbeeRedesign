namespace JixbeeRedesign.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public bool Unread { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
    }
}
