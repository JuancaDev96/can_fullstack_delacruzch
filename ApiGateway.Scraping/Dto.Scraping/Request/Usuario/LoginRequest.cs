using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Scraping.Request.Usuario
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El campo Usuario es requerido.")]
        public string Usuario { get; set; } = default!;
        [Required(ErrorMessage = "El campo Clave es requerido.")]
        public string Clave { get; set; } = default!;
    }
}
