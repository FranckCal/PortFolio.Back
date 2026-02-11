using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(string token, string refreshToken)> LoginAsync(string username, string password);
        Task<(string token, string refreshToken)> RefreshTokenAsync(string token, string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
    }
}
