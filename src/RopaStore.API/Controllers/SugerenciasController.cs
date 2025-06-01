using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaStore.Application.Interfaces;
using System.Security.Claims;

namespace RopaStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Cliente")]
    public class SugerenciasController : ControllerBase
    {
        private readonly ISugerenciaService _service;

        public SugerenciasController(ISugerenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId == null) return Unauthorized();

            var sugerencias = await _service.ObtenerSugerenciasParaClienteAsync(Guid.Parse(usuarioId));
            return Ok(sugerencias);
        }
    }
}
