using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB;

public partial class Institucion
{
    public int IdInstitucion { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Logo { get; set; }

    public string? Info { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public virtual ICollection<Informacion> Informacions { get; set; } = new List<Informacion>();
}
