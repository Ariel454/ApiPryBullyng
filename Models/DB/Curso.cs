using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB;

public partial class Curso
{
    public int IdCurso { get; set; }

    public int IdInstitucionF { get; set; }

    public int Nivel { get; set; }

    public string? Paralelo { get; set; }

    public virtual Institucion IdInstitucionFNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
