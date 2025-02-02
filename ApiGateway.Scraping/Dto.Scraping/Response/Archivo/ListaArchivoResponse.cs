using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Response.Archivo
{
    public class ListaArchivoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public bool? Procesado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;
        public int CantidadLinks { get; set; }
    }
}
