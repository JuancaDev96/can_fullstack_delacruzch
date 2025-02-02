using System;
using System.Collections.Generic;

namespace AccesoDatos.Scraping.Contexto;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;
}
