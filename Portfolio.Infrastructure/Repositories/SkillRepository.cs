// ========================================
// Portfolio.Infrastructure/Repositories/SkillRepository.cs
// ========================================
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly ApplicationDbContext _context;

    public SkillRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Skill>> GetAllAsync()
    {
        return await _context.Skills
            .OrderBy(s => s.Category)
            .ThenByDescending(s => s.ProficiencyLevel)
            .ToListAsync();
    }

    public async Task<IEnumerable<Skill>> GetByCategoryAsync(string category)
    {
        return await _context.Skills
            .Where(s => s.Category == category)
            .OrderByDescending(s => s.ProficiencyLevel)
            .ToListAsync();
    }
}