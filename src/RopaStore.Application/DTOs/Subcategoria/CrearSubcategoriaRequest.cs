namespace RopaStore.Application.DTOs.Subcategoria
{
    public class CrearSubcategoriaRequest
    {
        public string Nombre { get; set; } = null!;
        public Guid CategoriaId { get; set; }
    }
}
