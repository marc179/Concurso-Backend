namespace RopaStore.Application.DTOs.Pedido
{
    public class PedidoDto
    {
        public Guid PedidoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = null!;
        public List<string> Productos { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string CategoriaCliente { get; set; } = null!;
    }
}
