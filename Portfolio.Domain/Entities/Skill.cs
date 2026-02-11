using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // Backend, Frontend, Cloud, Database, etc.
        public int ProficiencyLevel { get; set; } // 1-5
        public int YearsOfExperience { get; set; }

        // Navigation property
        public Profile Profile { get; set; } = null!;
    }
}
