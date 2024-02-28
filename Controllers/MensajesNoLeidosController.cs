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
    public class MensajesNoLeidosController : Controller
    {
        private readonly AppbullyingContext _context;

        public MensajesNoLeidosController(AppbullyingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListarMensajesNoLeidos")]
        public async Task<IActionResult> listarMensajesNoLeidos()
        {

            List<MensajesNoLeidos> mensajesnoleidos = await _context.MensajesNoLeidos.ToListAsync();


            return Ok(mensajesnoleidos);

        }

        [HttpGet]
        [Route("BuscarMensajeNoLeido")]
        public async Task<IActionResult> ObtenerMensajeNoLeido(int id)
        {
            var mensajenoleido = await _context.MensajesNoLeidos.FindAsync(id);

            if (mensajenoleido == null)
            {
                return NotFound();
            }

            return Ok(mensajenoleido);
        }

        [HttpPost]
        [Route("RegistrarMensaje")]
        public async Task<IActionResult> GuardarMensajeNoLeido([FromQuery] int idUsuarioDestinatario, [FromQuery] int idUsuarioRemitente,  [FromQuery] int CantidadMensajes)
        {
            var mensajenoleido = new MensajesNoLeidos
            {
                IdUsuarioDestinatario = idUsuarioDestinatario,
                IdUsuarioRemitente = idUsuarioRemitente,
                CantidadMensajes = CantidadMensajes
            };

            _context.MensajesNoLeidos.Add(mensajenoleido);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Mensaje registrado",
                result = mensajenoleido
            });
        }



        // DELETE: api/Mensajes/5
        [HttpDelete]
        [Route("EliminarMensajeNoLeido")]
        public async Task<IActionResult> BorrarMensajeNoLeido(int id)
        {
            var mensajenoleido = await _context.MensajesNoLeidos.FindAsync(id);

            if (mensajenoleido == null)
            {
                return NotFound();
            }

            _context.MensajesNoLeidos.Remove(mensajenoleido);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Mensaje actualizado",
                result = mensajenoleido
            });
        }

        [HttpPut]
        [Route("ActualizarCantidadMensajesNoLeidos")]
        public async Task<IActionResult> ActualizarCantidadMensajesNoLeidos(int idUsuarioDestinatario, int idUsuarioRemitente, int nuevaCantidad)
        {
            var mensajeNoLeido = await _context.MensajesNoLeidos
                .FirstOrDefaultAsync(m => m.IdUsuarioDestinatario == idUsuarioDestinatario && m.IdUsuarioRemitente == idUsuarioRemitente);

            if (mensajeNoLeido == null)
            {
                return NotFound();
            }

            mensajeNoLeido.CantidadMensajes = nuevaCantidad;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Cantidad de mensajes no leídos actualizada",
                result = mensajeNoLeido
            });
        }

        [HttpGet]
        [Route("BuscarMensajesNoLeidosPorUsuario")]
        public async Task<IActionResult> BuscarMensajesNoLeidosPorUsuario(int idUsuarioDestinatario, int idUsuarioRemitente)
        {
            var mensajesNoLeidos = await _context.MensajesNoLeidos
                .Where(m => m.IdUsuarioDestinatario == idUsuarioDestinatario && m.IdUsuarioRemitente == idUsuarioRemitente)
                .ToListAsync();

            if (mensajesNoLeidos == null || !mensajesNoLeidos.Any())
            {
                return NotFound();
            }

            return Ok(mensajesNoLeidos);
        }

        [HttpGet]
        [Route("BuscarMensajesNoLeidosPorUsuarioDestinatario")]
        public async Task<IActionResult> BuscarMensajesNoLeidosPorUsuarioDestinatario(int idUsuarioDestinatario)
        {
            var mensajesNoLeidos = await _context.MensajesNoLeidos
                .Where(m => m.IdUsuarioDestinatario == idUsuarioDestinatario)
                .ToListAsync();

            if (mensajesNoLeidos == null || !mensajesNoLeidos.Any())
            {
                return NotFound("No se encontraron mensajes no leídos para el usuario destinatario especificado.");
            }

            return Ok(mensajesNoLeidos);
        }


        private bool MensajeExists(int id)
        {
            return (_context.MensajesNoLeidos?.Any(e => e.IdMensajeNoLeido == id)).GetValueOrDefault();
        }
    }
}
