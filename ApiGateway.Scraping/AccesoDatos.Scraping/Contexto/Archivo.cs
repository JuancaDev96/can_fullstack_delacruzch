using System;
using System.Collections.Generic;

namespace AccesoDatos.Scraping.Contexto;

public partial class Archivo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Procesado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public virtual ICollection<DireccionUrl> DireccionUrls { get; set; } = new List<DireccionUrl>();
}
