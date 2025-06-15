// Vehicheck.Core/Services/AuthService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Repositories.Interfaces;
using Vehicheck.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Vehicheck.Database.Context;

namespace Vehicheck.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly VehicheckDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(VehicheckDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse?> LoginAsync(UserLoginRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                .ThenInclude(c => c.CarManufacturer)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                .ThenInclude(c => c.CarModel)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                .ThenInclude(c => c.Components)
                .Where(u => u.DeletedAt == null)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return null;

            if (!user.VerifyPassword(request.Password))
                return null;

            var token = _jwtService.GenerateToken(user);
            var expiresAt = DateTime.UtcNow.AddHours(24);

            return new LoginResponse
            {
                Token = token,
                ExpiresAt = expiresAt,
                User = user.ToDto()
            };
        }
    }
}