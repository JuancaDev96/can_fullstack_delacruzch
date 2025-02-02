using AccesoDatos.Scraping.Contexto;
using AutoMapper;
using Dto.Scraping.Response;
using Dto.Scraping.Response.Archivo;
using Microsoft.EntityFrameworkCore;
using Repositorios.Scraping.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Implementaciones
{
    public class ArchivoRepositorio : RepositorioBase<Archivo>, IArchivoRepositorio
    {
        private readonly ScraperdbContext _contexto;
        public ArchivoRepositorio(ScraperdbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public async Task<ResponseBase<List<ListaArchivoResponse>>> Listar()
        {
            var respuesta = new ResponseBase<List<ListaArchivoResponse>>();

            try
            {
                var resultado = await ListAsync(
                    predicado: p => true,
                    selector: p => new ListaArchivoResponse
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        FechaRegistro = p.FechaRegistro,
                        CantidadLinks = p.DireccionUrls.Count,
                        Procesado = p.DireccionUrls.Count(p => !Convert.ToBoolean(p.Procesado)) == 0,
                        UsuarioRegistro = p.UsuarioRegistro
                    });

                respuesta.Data = resultado.ToList();
                respuesta.Success = true;
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = false;
            }
            return respuesta;
        }

        public async Task<ResponseBase<List<ListaDireccionesUrlResponse>>> ListarDireccionesByFileId(int idFile)
        {
            var respuesta = new ResponseBase<List<ListaDireccionesUrlResponse>>();

            try
            {
                var resultado = await _contexto.DireccionUrls
                    .Where(p => p.ArchivoId == idFile)
                    .Select(p => new ListaDireccionesUrlResponse
                    {
                        ArchivoId = p.ArchivoId,
                        Id = p.Id,
                        Procesado = p.Procesado,
                        Url = p.Url
                    }).ToListAsync();

                respuesta.Data = resultado;
                respuesta.Success = true;
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = false;
            }
            return respuesta;
        }

        public async Task<ResponseBase<List<ListaContenidoDireccionResponse>>> ListarContenidoByIdUrl(int idUrl)
        {
            var respuesta = new ResponseBase<List<ListaContenidoDireccionResponse>>();

            try
            {
                var resultado = await _contexto.DetalleDireccions
                    .Where(p => p.UrlId == idUrl && !string.IsNullOrEmpty(p.Contenido))
                    .Select(p => new ListaContenidoDireccionResponse
                    {
                        Id = p.Id,
                        Contenido = p.Contenido,
                        Etiqueta = p.Etiqueta
                    }).ToListAsync();

                respuesta.Data = resultado;
                respuesta.Success = true;
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = false;
            }
            return respuesta;
        }
    }
}
