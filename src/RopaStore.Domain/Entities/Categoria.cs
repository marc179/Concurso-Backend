namespace RopaStore.Domain.Entities
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public ICollection<Subcategoria> Subcategorias { get; set; } = new List<Subcategoria>();
    }
}
