// ========================================
// Portfolio.API/Controllers/ProjectController.cs
// ========================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
    {
        _projectService = projectService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère tous les projets (public)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        try
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des projets");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère un projet par ID (public)
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        try
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound($"Projet avec l'ID {id} introuvable");

            return Ok(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du projet");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Crée un projet (Admin uniquement)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        try
        {
            var created = await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du projet");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Met à jour un projet (Admin uniquement)
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
    {
        try
        {
            if (id != project.Id)
                return BadRequest("L'ID dans l'URL ne correspond pas à l'ID du projet");

            var updated = await _projectService.UpdateProjectAsync(project);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour du projet");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Supprime un projet (Admin uniquement)
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression du projet");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}