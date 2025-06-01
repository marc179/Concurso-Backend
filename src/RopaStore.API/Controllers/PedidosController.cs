using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaStore.Application.DTOs.Pedido;
using RopaStore.Application.Interfaces;
using System.Security.Claims;

namespace RopaStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidosController(IPedidoService service)
        {
            _service = service;
        }

        // Solo CLIENTE crea pedidos
        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CrearPedido([FromBody] CrearPedidoRequest request)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId == null) return Unauthorized();

            request.UsuarioId = Guid.Parse(usuarioId);

            var ok = await _service.CrearPedidoAsync(request);
            return ok ? Ok("Pedido realizado con éxito") : BadRequest("Error al procesar el pedido");
        }

        // Cliente ve sus pedidos
        [HttpGet("mis")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> MisPedidos()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId == null) return Unauthorized();

            var pedidos = await _service.ListarPedidosPorClienteAsync(Guid.Parse(usuarioId));
            return Ok(pedidos);
        }

        // Admin o Vendedor ven todos los pedidos
        [HttpGet]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> ListarTodos()
        {
            var pedidos = await _service.ListarTodosAsync();
            return Ok(pedidos);
        }
    }
}
