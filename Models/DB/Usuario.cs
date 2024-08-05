using System;
using System.Collections.Generic;

namespace ApiPryBullyng.Models.DB
{
    public partial class Usuario
    {
        public Usuario()
        {
            MensajesRemitidos = new HashSet<Mensaje>();
            MensajesRecibidos = new HashSet<Mensaje>();
            Formularios = new List<Formulario>();
            Resultados = new List<Resultado>();
        }

        public int IdUsuario { get; set; }
        public int IdCursoF { get; set; }
        public int Rol { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
        public int Genero { get; set; }
        public string Correo { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public virtual Curso IdCursoFNavigation { get; set; } = null!;
        public virtual ICollection<Formulario> Formularios { get; set; }
        public virtual ICollection<Resultado> Resultados { get; set; }
        public virtual ICollection<Mensaje> MensajesRemitidos { get; set; }
        public virtual ICollection<Mensaje> MensajesRecibidos { get; set; }
        public virtual ICollection<MensajesNoLeidos> MensajesNoLeidosRemitente { get; set; }
        public virtual ICollection<MensajesNoLeidos> MensajesNoLeidosDestinatario { get; set; }
    }
}
