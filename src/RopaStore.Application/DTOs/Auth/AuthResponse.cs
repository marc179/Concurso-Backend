namespace RopaStore.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
