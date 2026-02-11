// ========================================
// Portfolio.API/Controllers/ProfileController.cs
// ========================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
    {
        _profileService = profileService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère le profil public (pas d'authentification requise)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var profile = await _profileService.GetProfileAsync();
            if (profile == null)
                return NotFound("Profil introuvable");

            return Ok(profile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du profil");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Met à jour le profil (Admin uniquement)
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProfile([FromBody] Profile profile)
    {
        try
        {
            var updated = await _profileService.UpdateProfileAsync(profile);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour du profil");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}