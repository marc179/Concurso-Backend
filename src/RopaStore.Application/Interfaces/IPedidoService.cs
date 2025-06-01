using RopaStore.Application.DTOs.Pedido;

namespace RopaStore.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<bool> CrearPedidoAsync(CrearPedidoRequest request);
        Task<List<PedidoDto>> ListarPedidosPorClienteAsync(Guid usuarioId);
        Task<List<PedidoDto>> ListarTodosAsync(); // Admin o vendedor
    }
}
