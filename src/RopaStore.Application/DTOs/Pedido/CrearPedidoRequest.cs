namespace RopaStore.Application.DTOs.Pedido
{
    public class CrearPedidoRequest
    {
        public Guid UsuarioId { get; set; }
        public List<DetallePedidoItem> Productos { get; set; } = new();
    }
}
