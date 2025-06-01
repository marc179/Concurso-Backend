using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaStore.Application.DTOs.Producto;
using RopaStore.Application.Interfaces;

namespace RopaStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var productos = await _service.ListarAsync();
            return Ok(productos);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Crear([FromBody] CrearProductoRequest request)
        {
            var ok = await _service.CrearAsync(request);
            return ok ? Ok("Producto creado") : BadRequest("Error al crear");
        }

        [HttpPut]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarProductoRequest request)
        {
            var ok = await _service.ActualizarAsync(request);
            return ok ? Ok("Producto actualizado") : NotFound("Producto no encontrado");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var ok = await _service.EliminarAsync(id);
            return ok ? Ok("Producto eliminado") : NotFound("No existe");
        }
    }
}
