using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Request.Usuario
{
    public class UsuarioRequest
    {
        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string Clave { get; set; } = null!;
    }
}
