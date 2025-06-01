namespace RopaStore.Domain.Entities
{
    public class HistorialProductoCliente
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        public string Accion { get; set; } = null!; // "visto", "comprado", "descartado"
        public int Calificacion { get; set; } // 1 a 5 estrellas
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
