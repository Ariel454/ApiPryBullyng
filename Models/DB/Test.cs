using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB;

public partial class Test
{
    public int IdTest { get; set; }

    public string NombreTest { get; set; } = null!;

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();

    public virtual ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
