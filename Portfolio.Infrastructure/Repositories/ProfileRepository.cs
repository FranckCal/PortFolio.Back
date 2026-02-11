// ========================================
// Portfolio.Infrastructure/Repositories/ProfileRepository.cs
// ========================================
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly ApplicationDbContext _context;

    public ProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Profile?> GetAsync()
    {
        return await _context.Profiles
            .Include(p => p.Experiences)
            .Include(p => p.Educations)
            .Include(p => p.Certifications)
            .Include(p => p.Skills)
            .FirstOrDefaultAsync();
    }

    public async Task<Profile> UpdateAsync(Profile profile)
    {
        _context.Profiles.Update(profile);
        return profile;
    }
}