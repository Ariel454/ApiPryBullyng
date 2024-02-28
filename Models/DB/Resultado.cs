using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB;

public partial class Resultado
{
    public int IdResultado { get; set; }

    public int? IdTest { get; set; }

    public int? IdUsuario { get; set; }

    public int? VecesSi { get; set; }

    public int? VecesNo { get; set; }

    public virtual Test? IdTestNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
