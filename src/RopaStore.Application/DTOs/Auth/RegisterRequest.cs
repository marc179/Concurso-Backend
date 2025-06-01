namespace RopaStore.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public Guid RolId { get; set; }
    }
}
