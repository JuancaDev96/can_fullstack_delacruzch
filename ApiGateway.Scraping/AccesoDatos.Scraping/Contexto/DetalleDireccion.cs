using System;
using System.Collections.Generic;

namespace AccesoDatos.Scraping.Contexto;

public partial class DetalleDireccion
{
    public int Id { get; set; }

    public string Etiqueta { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public int? UrlId { get; set; }

    public virtual DireccionUrl? Url { get; set; }
}
