namespace RopaStore.Domain.Entities
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string ImagenUrl { get; set; } = null!;
        public string Talla { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Marca { get; set; } = null!;
        public int Stock { get; set; }
 
        public Guid SubcategoriaId { get; set; }
        public Subcategoria Subcategoria { get; set; } = null!;
    }
}
