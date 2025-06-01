namespace RopaStore.Application.DTOs.Subcategoria
{
    public class SubcategoriaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public Guid CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = null!;
    }
}
