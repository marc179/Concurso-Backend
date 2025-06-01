using RopaStore.Application.DTOs.Categoria;

namespace RopaStore.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> ListarAsync();
        Task<bool> CrearAsync(CrearCategoriaRequest request);
        Task<bool> ActualizarAsync(Guid id, CrearCategoriaRequest request);
        Task<bool> EliminarAsync(Guid id);
    }
}
