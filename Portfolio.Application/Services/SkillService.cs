// ========================================
// Portfolio.Application/Services/SkillService.cs
// ========================================
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services;

public class SkillService : ISkillService
{
    private readonly IUnitOfWork _unitOfWork;

    public SkillService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
    {
        return await _unitOfWork.Skills.GetAllAsync();
    }

    public async Task<IEnumerable<Skill>> GetSkillsByCategoryAsync(string category)
    {
        return await _unitOfWork.Skills.GetByCategoryAsync(category);
    }
}