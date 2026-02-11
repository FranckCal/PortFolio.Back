using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Application.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile?> GetAsync();
        Task<Profile> UpdateAsync(Profile profile);
    }
}
