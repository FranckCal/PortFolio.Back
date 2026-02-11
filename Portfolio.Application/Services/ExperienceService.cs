// Portfolio.Application/Services/ExperienceService.cs
// ========================================
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services;

public class ExperienceService : IExperienceService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExperienceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        return await _unitOfWork.Experiences.GetAllAsync();
    }

    public async Task<Experience?> GetExperienceByIdAsync(int id)
    {
        return await _unitOfWork.Experiences.GetByIdAsync(id);
    }
}