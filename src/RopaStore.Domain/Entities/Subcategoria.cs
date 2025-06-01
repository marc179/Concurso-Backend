namespace RopaStore.Domain.Entities
{
    public class Subcategoria
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
