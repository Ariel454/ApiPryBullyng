using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB;

public partial class Formulario
{
    public int IdFormulario { get; set; }

    public int? IdUsuarioF { get; set; }

    public string TituloCaso { get; set; } = null!;

    public string? Detalle { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Usuario? IdUsuarioFNavigation { get; set; }
}
