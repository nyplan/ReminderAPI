using ReminderAPI.Entities;

namespace ReminderAPI.DTOs.ReminderDTOs
{
    public record ReminderToAddDto
    {
        public string Recipient { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Method { get; set; }
    }
}
