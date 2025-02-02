using System;
using System.Collections.Generic;

namespace AccesoDatos.Scraping.Contexto;

public partial class DireccionUrl
{
    public int Id { get; set; }

    public string Url { get; set; } = null!;

    public bool? Procesado { get; set; }

    public int? ArchivoId { get; set; }

    public virtual Archivo? Archivo { get; set; }

    public virtual ICollection<DetalleDireccion> DetalleDireccions { get; set; } = new List<DetalleDireccion>();
}
