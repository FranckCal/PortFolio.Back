// ========================================
// Portfolio.API/Controllers/ExperienceController.cs
// ========================================
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceService _experienceService;
    private readonly ILogger<ExperienceController> _logger;

    public ExperienceController(IExperienceService experienceService, ILogger<ExperienceController> logger)
    {
        _experienceService = experienceService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère toutes les expériences
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetExperiences()
    {
        try
        {
            var experiences = await _experienceService.GetAllExperiencesAsync();
            return Ok(experiences);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des expériences");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère une expérience par ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetExperienceById(int id)
    {
        try
        {
            var experience = await _experienceService.GetExperienceByIdAsync(id);
            if (experience == null)
                return NotFound($"Expérience avec l'ID {id} introuvable");

            return Ok(experience);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération de l'expérience");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}