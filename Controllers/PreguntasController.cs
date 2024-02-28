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
    public class PreguntasController : ControllerBase
    {
        private readonly AppbullyingContext _context;

        public PreguntasController(AppbullyingContext context)
        {
            _context = context;
        }

        // GET: api/Preguntas
        [HttpGet]
        [Route("ListarPreguntas")]
        public async Task<IActionResult> listarPreguntas()
        {

            List<Preguntum> preguntas = await _context.Pregunta.ToListAsync();


            return Ok(preguntas);

        }

        // GET: api/Preguntas/5
        [HttpGet]
        [Route("BuscarPregunta")]
        public async Task<IActionResult> ObtenerPregunta(int id)
        {
            var pregunta = await _context.Pregunta.FindAsync(id);

            if (pregunta == null)
            {
                return NotFound();
            }

            return Ok(pregunta);
        }

        // PUT: api/Preguntas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("RegistrarPregunta")]
        public async Task<IActionResult> guardarPregunta([FromQuery] string Texto, [FromQuery] int idTest)
        {
            var pregunta = new Preguntum();

            pregunta.TextoPregunta = Texto;
            pregunta.IdTestF = idTest;

            _context.Pregunta.Add(pregunta);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Pregunta registrada",
                result = pregunta
            });
        }


        // DELETE: api/Mensajes/5
        [HttpDelete]
        [Route("EliminarPregunta")]
        public async Task<IActionResult> BorrarPregunta(int id)
        {
            var pregunta = await _context.Pregunta.FindAsync(id);

            if (pregunta == null)
            {
                return NotFound();
            }

            _context.Pregunta.Remove(pregunta);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Mensaje actualizado",
                result = pregunta
            });
        }

        private bool PreguntumExists(int id)
        {
            return (_context.Pregunta?.Any(e => e.IdPregunta == id)).GetValueOrDefault();
        }
    }
}
