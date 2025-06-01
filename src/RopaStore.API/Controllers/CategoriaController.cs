using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaStore.Application.DTOs.Categoria;
using RopaStore.Application.Interfaces;

namespace RopaStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _service.ListarAsync();
            return Ok(categorias);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear([FromBody] CrearCategoriaRequest request)
        {
            var ok = await _service.CrearAsync(request);
            return ok ? Ok("Categoría creada") : BadRequest("Ya existe");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] CrearCategoriaRequest request)
        {
            var ok = await _service.ActualizarAsync(id, request);
            return ok ? Ok("Categoría actualizada") : NotFound("No existe");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var ok = await _service.EliminarAsync(id);
            return ok ? Ok("Categoría eliminada") : BadRequest("No se puede eliminar");
        }
    }
}
