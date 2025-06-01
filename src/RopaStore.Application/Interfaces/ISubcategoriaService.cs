using RopaStore.Application.DTOs.Subcategoria;

namespace RopaStore.Application.Interfaces
{
    public interface ISubcategoriaService
    {
        Task<List<SubcategoriaDto>> ListarTodasAsync();
        Task<List<SubcategoriaDto>> ListarPorCategoriaAsync(Guid categoriaId);
        Task<bool> CrearAsync(CrearSubcategoriaRequest request);
        Task<bool> ActualizarAsync(Guid id, CrearSubcategoriaRequest request);
        Task<bool> EliminarAsync(Guid id);
    }
}
