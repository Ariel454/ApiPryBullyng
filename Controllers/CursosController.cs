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
    public class CursosController : ControllerBase
    {
        private readonly AppbullyingContext _context;

        public CursosController(AppbullyingContext context)
        {
            _context = context;
        }

        // GET: api/Cursoes
        [HttpGet]
        [Route("ListarCursos")]
        public async Task<IActionResult> listarCursos()
        {

            List<Curso> cursos = await _context.Cursos.ToListAsync();


            return Ok(cursos);

        }

        // GET: api/Cursoes/5
        [HttpGet]
        [Route("BuscarCurso")]
        public async Task<IActionResult> ObtenerCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        // PUT: api/Cursoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("EditarCurso")]
        public async Task<IActionResult> EditarCurso(int id, [FromQuery] int Institucion,
            [FromQuery] int Nivel, [FromQuery] string Paralelo)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del usuario existente con los valores del usuario actualizado
            curso.IdInstitucionF = Institucion;
            curso.Nivel = Nivel;
            curso.Paralelo = Paralelo;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Curso actualizado",
                result = curso
            });
        }

        // POST: api/Cursoes
        [HttpPost]
        [Route("RegistrarCurso")]
        public async Task<IActionResult> guardarCurso([FromQuery] int Institucion,
            [FromQuery] int Nivel, [FromQuery] string Paralelo)
        {
            var curso = new Curso();

            curso.IdInstitucionF = Institucion;
            curso.Nivel = Nivel;
            curso.Paralelo = Paralelo;


            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Curso registrado",
                result = curso
            });
        }

        // DELETE: api/Cursoes/5
        [HttpDelete]
        [Route("EliminarCurso")]
        public async Task<IActionResult> BorrarCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Curso eliminado",
                result = curso
            });
        }

    }
}
