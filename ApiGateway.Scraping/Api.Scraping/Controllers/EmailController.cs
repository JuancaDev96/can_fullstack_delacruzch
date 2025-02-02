using Dto.Scraping.Request.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorios.Scraping.Interfaces;

namespace Api.Scraping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _servicio;

        public EmailController(IEmailService servicio)
        {
            _servicio = servicio;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmailRequest request)
        {
            var resultado = await _servicio.EnviarCorreo(request);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
