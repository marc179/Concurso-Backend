using RopaStore.Application.DTOs.Sugerencia;

namespace RopaStore.Application.Interfaces
{
    public interface ISugerenciaService
    {
        Task<List<ProductoSugeridoDto>> ObtenerSugerenciasParaClienteAsync(Guid usuarioId);
    }
}
