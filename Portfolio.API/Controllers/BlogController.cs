// ========================================
// Portfolio.API/Controllers/BlogController.cs
// ========================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly ILogger<BlogController> _logger;

    public BlogController(IBlogService blogService, ILogger<BlogController> logger)
    {
        _blogService = blogService;
        _logger = logger;
    }

    /// <summary>
    /// Récupère tous les articles publiés (public)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        try
        {
            var posts = await _blogService.GetAllPostsAsync(publishedOnly: true);
            return Ok(posts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des articles");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère tous les articles incluant brouillons (Admin uniquement)
    /// </summary>
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllPosts()
    {
        try
        {
            var posts = await _blogService.GetAllPostsAsync(publishedOnly: false);
            return Ok(posts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération de tous les articles");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Récupère un article par son slug (public)
    /// </summary>
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetPostBySlug(string slug)
    {
        try
        {
            var post = await _blogService.GetPostBySlugAsync(slug);
            if (post == null)
                return NotFound($"Article '{slug}' introuvable");

            post.ViewCount++;
            await _blogService.UpdatePostAsync(post);

            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération de l'article");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Crée un article (Admin uniquement)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePost([FromBody] BlogPost post)
    {
        try
        {
            var created = await _blogService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostBySlug), new { slug = created.Slug }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création de l'article");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Met à jour un article (Admin uniquement)
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] BlogPost post)
    {
        try
        {
            if (id != post.Id)
                return BadRequest("L'ID ne correspond pas");

            var updated = await _blogService.UpdatePostAsync(post);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour de l'article");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Supprime un article (Admin uniquement)
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePost(int id)
    {
        try
        {
            await _blogService.DeletePostAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression de l'article");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}