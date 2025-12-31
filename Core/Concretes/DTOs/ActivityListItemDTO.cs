using Core.Concretes.Enums;

namespace Core.Concretes.DTOs
{
    public class ActivityListItemDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; }
        public ActivityType Type { get; set; }
    }
}
