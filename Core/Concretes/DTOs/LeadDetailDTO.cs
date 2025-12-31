using Core.Concretes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.DTOs
{
    public class LeadDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Notes { get; set; }
        public int? ConvertedCustomerId { get; set; }
        public DateTime? ConvertedDate { get; set; }
        public string? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public LeadSource Source { get; set; }
        public LeadStatus Status { get; set; }
        public IEnumerable<ActivityListItemDTO> Activities { get; set; } = [];
    }
}
