using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaStore.Application.DTOs.Subcategoria;
using RopaStore.Application.Interfaces;

namespace RopaStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcategoriasController : ControllerBase
    {
        private readonly ISubcategoriaService _service;

        public SubcategoriasController(ISubcategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarTodasAsync();
            return Ok(lista);
        }

        [HttpGet("por-categoria/{categoriaId}")]
        public async Task<IActionResult> ListarPorCategoria(Guid categoriaId)
        {
            var lista = await _service.ListarPorCategoriaAsync(categoriaId);
            return Ok(lista);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear([FromBody] CrearSubcategoriaRequest request)
        {
            var ok = await _service.CrearAsync(request);
            return ok ? Ok("Subcategoría creada") : BadRequest("Ya existe");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] CrearSubcategoriaRequest request)
        {
            var ok = await _service.ActualizarAsync(id, request);
            return ok ? Ok("Subcategoría actualizada") : NotFound("No existe");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var ok = await _service.EliminarAsync(id);
            return ok ? Ok("Eliminada") : BadRequest("No se puede eliminar");
        }
    }
}
