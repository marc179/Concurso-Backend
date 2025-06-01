namespace RopaStore.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string ContrasenaHash { get; set; } = null!;
        public bool EstaActivo { get; set; } = true;

        public Guid RolId { get; set; }
        public Rol Rol { get; set; } = null!;

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<HistorialProductoCliente> Historial { get; set; } = new List<HistorialProductoCliente>();
    }
}
