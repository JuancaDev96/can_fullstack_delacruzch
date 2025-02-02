using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Response.Usuario
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;
        public string Nombre { get; set; } = default!;
    }
}
