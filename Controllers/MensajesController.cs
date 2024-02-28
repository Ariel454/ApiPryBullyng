using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApiPryBullyng.Models.DB;

namespace ApiPryBullyng.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly AppbullyingContext _context;

        public MensajesController(AppbullyingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListarMensajes")]
        public async Task<IActionResult> listarMensajes()
        {

            List<Mensaje> mensajes = await _context.Mensajes.ToListAsync();


            return Ok(mensajes);

        }

        [HttpGet]
        [Route("BuscarMensaje")]
        public async Task<IActionResult> ObtenerMensaje(int id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);

            if (mensaje == null)
            {
                return NotFound();
            }

            return Ok(mensaje);
        }

        [HttpGet]
        [Route("ObtenerMensajesPorGrupo")]
        public async Task<IActionResult> ObtenerMensajesPorGrupo(string groupId)
        {
            try
            {
                var mensajes = await _context.Mensajes.Where(m => m.GrupoId == groupId).ToListAsync();

                if (mensajes == null || mensajes.Count == 0)
                {
                    return NotFound("No se encontraron mensajes para el grupo especificado.");
                }

                return Ok(mensajes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los mensajes: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("RegistrarMensaje")]
        public async Task<IActionResult> GuardarMensaje([FromQuery] int idUsuarioRemitente, [FromQuery] int idUsuarioDestinatario, [FromQuery] string contenido, [FromQuery] string grupoId)
        {
            var mensaje = new Mensaje
            {
                IdUsuarioRemitente = idUsuarioRemitente,
                IdUsuarioDestinatario = idUsuarioDestinatario,
                Contenido = contenido,
                FechaEnvio = DateTime.Now, // Supongamos que la fecha de envío es la actual
                Leido = false, // Se marca como no leído inicialmente
                GrupoId = grupoId
            };

            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Mensaje registrado",
                result = mensaje
            });
        }

        [HttpPut]
        [Route("MarcarMensajesComoLeidosPorGrupo")]
        public async Task<IActionResult> MarcarMensajesComoLeidosPorGrupo(string groupId)
        {
            try
            {
                // Buscar todos los mensajes del grupo que aún no han sido marcados como leídos
                var mensajesPorGrupo = await _context.Mensajes.Where(m => m.GrupoId == groupId && !m.Leido).ToListAsync();

                // Validar si hay mensajes por marcar como leídos
                if (mensajesPorGrupo.Any())
                {
                    // Marcar los mensajes como leídos
                    foreach (var mensaje in mensajesPorGrupo)
                    {
                        mensaje.Leido = true;
                    }

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = $"Mensajes del grupo {groupId} marcados como leídos correctamente."
                    });
                }
                else
                {
                    // No hay mensajes por marcar como leídos
                    return NotFound($"No se encontraron mensajes por marcar como leídos en el grupo {groupId}.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                return StatusCode(500, $"Error al marcar los mensajes como leídos por grupo: {ex.Message}");
            }
        }


        // DELETE: api/Mensajes/5
        [HttpDelete]
        [Route("EliminarMensaje")]
        public async Task<IActionResult> BorrarMensaje(int id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);

            if (mensaje == null)
            {
                return NotFound();
            }

            _context.Mensajes.Remove(mensaje);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Mensaje actualizado",
                result = mensaje
            });
        }

        private bool MensajeExists(int id)
        {
            return (_context.Mensajes?.Any(e => e.IdMensaje == id)).GetValueOrDefault();
        }
    }
}
