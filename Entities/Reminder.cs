namespace ReminderAPI.Entities
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
        public bool Sent { get; set; }
    }
}
