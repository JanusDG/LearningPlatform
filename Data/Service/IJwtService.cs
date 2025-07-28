using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;

namespace LearningPlatform.Data.Service
{

    public interface IJwtService
    {
        Task<LoginResponseModel?> AuthenticateAsync(LoginRequestModel loginRequest);
        string GenerateToken(string username, string role, int id);
    }
}