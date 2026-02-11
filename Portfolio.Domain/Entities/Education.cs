using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Domain.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Institution { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string FieldOfStudy { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation property
        public Profile Profile { get; set; } = null!;
    }
}
