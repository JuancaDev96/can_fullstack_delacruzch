using Dto.Scraping.Response.Archivo;
using Dto.Scraping.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Interfaces
{
    public  interface IArchivoRepositorio
    {
        Task<ResponseBase<List<ListaArchivoResponse>>> Listar();
        Task<ResponseBase<List<ListaDireccionesUrlResponse>>> ListarDireccionesByFileId(int idFile);
        Task<ResponseBase<List<ListaContenidoDireccionResponse>>> ListarContenidoByIdUrl(int idUrl);
    }
}
