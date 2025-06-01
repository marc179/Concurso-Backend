namespace RopaStore.Application.DTOs.Producto
{
    public class ProductoDto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Marca { get; set; } = null!;
        public int Stock { get; set; }
    }
}
