// ========================================
// Portfolio.Application/Services/ProfileService.cs
// ========================================
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Profile?> GetProfileAsync()
    {
        return await _unitOfWork.Profiles.GetAsync();
    }

    public async Task<Profile> UpdateProfileAsync(Profile profile)
    {
        profile.UpdatedAt = DateTime.UtcNow;
        var updated = await _unitOfWork.Profiles.UpdateAsync(profile);
        await _unitOfWork.SaveChangesAsync();
        return updated;
    }
}