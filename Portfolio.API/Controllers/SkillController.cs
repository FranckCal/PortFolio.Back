
// ========================================
// Portfolio.API/Controllers/SkillController.cs
// ========================================
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;
    private readonly ILogger<SkillController> _logger;

    public SkillController(ISkillService skillService, ILogger<SkillController> logger)
    {
        _skillService = skillService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère toutes les compétences
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetSkills()
    {
        try
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des compétences");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère les compétences par catégorie (Backend, Frontend, Cloud, etc.)
    /// </summary>
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetSkillsByCategory(string category)
    {
        try
        {
            var skills = await _skillService.GetSkillsByCategoryAsync(category);
            return Ok(skills);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des compétences par catégorie");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}