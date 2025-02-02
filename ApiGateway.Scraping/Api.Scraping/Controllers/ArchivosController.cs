using Dto.Scraping.Request.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorios.Scraping.Interfaces;

namespace Api.Scraping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController(IArchivoRepositorio repositorio) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await repositorio.Listar();
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpGet("DireccionesByFile/{idFile:int}")]
        public async Task<IActionResult> GetDireccionesByFile(int idFile)
        {
            var resultado = await repositorio.ListarDireccionesByFileId(idFile);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpGet("ContenidoByUrl/{idUrl:int}")]
        public async Task<IActionResult> GetContenidoByUrl(int idUrl)
        {
            var resultado = await repositorio.ListarContenidoByIdUrl(idUrl);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
