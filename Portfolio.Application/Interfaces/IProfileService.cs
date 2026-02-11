using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Application.Interfaces
{
    public interface IProfileService
    {
        Task<Profile?> GetProfileAsync();
        Task<Profile> UpdateProfileAsync(Profile profile);
    }
}
