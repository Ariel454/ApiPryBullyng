using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB;

public partial class Preguntum
{
    public int IdPregunta { get; set; }

    public string? TextoPregunta { get; set; }

    public int? IdTestF { get; set; }

    public virtual Test? IdTestFNavigation { get; set; }
}
