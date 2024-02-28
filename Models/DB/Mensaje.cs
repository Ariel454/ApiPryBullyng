using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB;

public partial class Mensaje
{
    public int IdMensaje { get; set; }

    public int IdUsuarioRemitente { get; set; }

    public int IdUsuarioDestinatario { get; set; }

    public string Contenido { get; set; } = null!;

    public DateTime FechaEnvio { get; set; }

    public bool Leido { get; set; }

    public string GrupoId { get; set; }

    public virtual Usuario IdUsuarioRemitenteNavigation { get; set; }

    public virtual Usuario IdUsuarioDestinatarioNavigation { get; set; }
}

