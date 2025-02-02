using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Response.Archivo
{
    public class ListaDireccionesUrlResponse
    {
        public int Id { get; set; }

        public string Url { get; set; } = null!;

        public bool? Procesado { get; set; }

        public int? ArchivoId { get; set; }
    }
}
