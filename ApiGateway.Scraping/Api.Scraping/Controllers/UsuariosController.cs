using Dto.Scraping.Request.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorios.Scraping.Interfaces;

namespace Api.Scraping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(IUsuarioRepositorio repositorio) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(UsuarioRequest request)
        {
            var resultado = await repositorio.Registrar(request);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin(LoginRequest request)
        {
            var resultado = await repositorio.Login(request);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
