using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Response.Archivo
{
    public class ListaContenidoDireccionResponse
    {
        public int Id { get; set; }

        public string Etiqueta { get; set; } = null!;

        public string Contenido { get; set; } = null!;
    }
}
