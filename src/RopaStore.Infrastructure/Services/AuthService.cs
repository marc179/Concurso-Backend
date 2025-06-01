using RopaStore.Application.DTOs.Auth;
using RopaStore.Application.Interfaces;
using RopaStore.Domain.Entities;
using RopaStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RopaStore.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly RopaStoreDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(RopaStoreDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == request.Correo);

            if (usuario == null) return null;

            var hashed = HashPassword(request.Contrasena);
            if (usuario.ContrasenaHash != hashed) return null;

            var token = GenerarToken(usuario);

            return new AuthResponse
            {
                Token = token,
                NombreCompleto = usuario.NombreCompleto,
                Correo = usuario.Correo,
                Rol = usuario.Rol.Nombre
            };
        }

        public async Task<bool> RegistrarAsync(RegisterRequest request)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo);
            if (existe) return false;

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                NombreCompleto = request.NombreCompleto,
                Correo = request.Correo,
                ContrasenaHash = HashPassword(request.Contrasena),
                RolId = request.RolId,
                EstaActivo = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
