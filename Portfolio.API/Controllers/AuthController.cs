// ========================================
// Portfolio.API/Controllers/AuthController.cs
// ========================================
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Models;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Connexion Admin - Retourne un JWT token
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username et Password sont obligatoires");

            var (token, refreshToken) = await _authService.LoginAsync(request.Username, request.Password);

            return Ok(new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresIn = 60 // minutes
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Identifiants invalides");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la connexion");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    /// <summary>
    /// Rafraîchissement du token JWT
    /// </summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Token et RefreshToken sont obligatoires");

            var (token, refreshToken) = await _authService.RefreshTokenAsync(request.Token, request.RefreshToken);

            return Ok(new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresIn = 60
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Token invalide ou expiré");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du rafraîchissement du token");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}

 