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
    public class ResultadosController : ControllerBase
    {
        private readonly AppbullyingContext _context;

        public ResultadosController(AppbullyingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListarResultados")]
        public async Task<IActionResult> listarResultados()
        {

            List<Resultado> resultados = await _context.Resultados.ToListAsync();


            return Ok(resultados);

        }

        [HttpGet]
        [Route("BuscarResultado")]
        public async Task<IActionResult> ObtenerResultado(int id)
        {
            var resultado = await _context.Resultados.FindAsync(id);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        [HttpPost]
        [Route("RegistrarResultado")]
        public async Task<IActionResult> GuardarMensajeNoLeido([FromQuery] int idTest, [FromQuery] int idUsuario, [FromQuery] int PuntajeResultados)
        {
            var resultado = new Resultado
            {
                IdTest = idTest,
                IdUsuario = idUsuario,               
                PuntajeResultados = PuntajeResultados
            };

            _context.Resultados.Add(resultado);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Mensaje registrado",
                result = resultado
            });
        }



        // DELETE: api/Mensajes/5
        [HttpDelete]
        [Route("EliminarResultado")]
        public async Task<IActionResult> BorrarResultado(int id)
        {
            var resultado = await _context.Resultados.FindAsync(id);

            if (resultado == null)
            {
                return NotFound();
            }

            _context.Resultados.Remove(resultado);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Mensaje actualizado",
                result = resultado
            });
        }

        [HttpGet]
        [Route("ListarResultadosPorCursoYTest")]
        public async Task<IActionResult> listarResultadosPorCursoYTest(int idCurso, int idTest)
        {
            // Filtrar los resultados por el ID del curso y el ID del test
            var resultados = await _context.Resultados
                .Where(r => _context.Usuarios.Any(u => u.IdUsuario == r.IdUsuario && u.IdCursoF == idCurso) && r.IdTest == idTest)
                .ToListAsync();

            return Ok(resultados);
        }



        private bool ResultadoExists(int id)
        {
            return (_context.Resultados?.Any(e => e.IdResultado == id)).GetValueOrDefault();
        }
    }
}
