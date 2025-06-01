using Microsoft.AspNetCore.Mvc;

using RopaStore.Application.DTOs.Auth;

using RopaStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

 
namespace RopaStore.API.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class AuthController : ControllerBase

    {

        private readonly IAuthService _authService;
 
        public AuthController(IAuthService authService)

        {

            _authService = authService;

        }
 
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequest request)

        {

            var resultado = await _authService.RegistrarAsync(request);

            if (!resultado)

                return BadRequest("El usuario ya existe");
 
            return Ok("Usuario registrado correctamente");

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
            return Unauthorized("Credenciales incorrectas");

            return Ok(result);
        }
        [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class ProductosController : ControllerBase
        {
    // Métodos aquí
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost("crear")]
        public IActionResult CrearProducto()
        {
            return Ok("Solo admin puede crear productos.");
        }
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("stock")]
        public IActionResult VerStock()
        {
            return Ok("Stock visible solo para vendedor o admin.");
        }





    }

}

 