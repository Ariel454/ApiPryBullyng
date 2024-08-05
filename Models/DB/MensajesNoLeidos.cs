using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB
{
    public partial class MensajesNoLeidos
    {
        public int IdMensajeNoLeido { get; set; }

        public int IdUsuarioDestinatario { get; set; }

        public int IdUsuarioRemitente { get; set; }

        public int CantidadMensajes { get; set; }

        public virtual Usuario? IdUsuarioDestinatarioNavigation { get; set; }

        public virtual Usuario? IdUsuarioRemitenteNavigation { get; set; }
    }
}
