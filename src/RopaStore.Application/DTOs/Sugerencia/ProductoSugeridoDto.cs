namespace RopaStore.Application.DTOs.Sugerencia
{
    public class ProductoSugeridoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Marca { get; set; } = null!;
        public string ImagenUrl { get; set; } = null!;
    }
}
