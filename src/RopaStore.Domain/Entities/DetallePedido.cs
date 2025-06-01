namespace RopaStore.Domain.Entities
{
    public class DetallePedido
    {
        public Guid Id { get; set; }

        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
