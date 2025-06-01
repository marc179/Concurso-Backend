namespace RopaStore.Domain.Entities
{
    public class Factura
    {
        public Guid Id { get; set; }

        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; } // en porcentaje, ej. 0.15
        public decimal Total { get; set; }
        public string CategoriaCliente { get; set; } = null!;
    }
}
