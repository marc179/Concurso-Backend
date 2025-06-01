namespace RopaStore.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

        public Factura? Factura { get; set; }
    }
}
