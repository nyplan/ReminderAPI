using ReminderAPI.Entities;

namespace ReminderAPI.DTOs.ReminderDTOs
{
    public record ReminderToReadDto
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
        public bool Sent { get; set; }
    }
}
