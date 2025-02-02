using AccesoDatos.Scraping.Contexto;
using AutoMapper;
using Dto.Scraping.Request.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Mappers
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioRequest, Usuario>();
        }
    }
}
