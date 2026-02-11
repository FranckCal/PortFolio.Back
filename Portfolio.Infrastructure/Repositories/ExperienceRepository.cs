// ========================================
// Portfolio.Infrastructure/Repositories/ExperienceRepository.cs
// ========================================
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly ApplicationDbContext _context;

    public ExperienceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Experience>> GetAllAsync()
    {
        return await _context.Experiences
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<Experience?> GetByIdAsync(int id)
    {
        return await _context.Experiences.FindAsync(id);
    }
}