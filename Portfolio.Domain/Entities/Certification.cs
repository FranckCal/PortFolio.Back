using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Domain.Entities
{
    public class Certification
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int Year { get; set; }
        public string CredentialUrl { get; set; } = string.Empty;

        // Navigation property
        public Profile Profile { get; set; } = null!;
    }
}
