using RopaStore.Application.DTOs.Producto;

namespace RopaStore.Application.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ListarAsync();
        Task<bool> CrearAsync(CrearProductoRequest request);
        Task<bool> ActualizarAsync(ActualizarProductoRequest request);
        Task<bool> EliminarAsync(Guid id);
    }
}
