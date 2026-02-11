// ========================================
// Portfolio.Application/Services/ProjectService.cs
// ========================================
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await _unitOfWork.Projects.GetAllAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await _unitOfWork.Projects.GetByIdAsync(id);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        project.CreatedAt = DateTime.UtcNow;
        project.UpdatedAt = DateTime.UtcNow;
        var created = await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();
        return created;
    }

    public async Task<Project> UpdateProjectAsync(Project project)
    {
        project.UpdatedAt = DateTime.UtcNow;
        var updated = await _unitOfWork.Projects.UpdateAsync(project);
        await _unitOfWork.SaveChangesAsync();
        return updated;
    }

    public async Task DeleteProjectAsync(int id)
    {
        await _unitOfWork.Projects.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}