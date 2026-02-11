using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Domain.Entities
{
    public class Experience
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Technologies { get; set; } = string.Empty; // JSON array
        public string Achievements { get; set; } = string.Empty; // JSON array
        public int Order { get; set; }

        // Navigation property
        public Profile Profile { get; set; } = null!;
    }
}
