using AccesoDatos.Scraping.Contexto;
using Dto.Scraping.Request.Usuario;
using Dto.Scraping.Response;
using Dto.Scraping.Response.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Task<ResponseBase> Registrar(UsuarioRequest request);
        Task<ResponseBase<LoginResponse>> Login(LoginRequest request);
    }
}
