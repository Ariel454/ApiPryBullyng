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
    public class InstitucionsController : ControllerBase
    {
        private readonly AppbullyingContext _context;

        public InstitucionsController(AppbullyingContext context)
        {
            _context = context;
        }

        // GET: api/Institucions
        [HttpGet]
        [Route("ListarInstituciones")]
        public async Task<IActionResult> listarInstitucion()
        {

            List<Institucion> instituciones = await _context.Institucions.ToListAsync();


            return Ok(instituciones);

        }

        // GET: api/Institucions/5
        [HttpGet]
        [Route("BuscarInstitucion")]
        public async Task<IActionResult> ObtenerInstitucion(int id)
        {
            var institucion = await _context.Institucions.FindAsync(id);

            if (institucion == null)
            {
                return NotFound();
            }

            return Ok(institucion);
        }

        // PUT: api/Institucions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("EditarInstitucion")]
        public async Task<IActionResult> EditarInstitucion(int id, [FromQuery] string Nombre, [FromQuery] string Logo,
            [FromQuery] string Info)
        {
            var institucion = await _context.Institucions.FindAsync(id);

            if (institucion == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del usuario existente con los valores del usuario actualizado
            institucion.Nombre = Nombre;
            institucion.Logo = Logo;
            institucion.Info = Info;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Institución actualizada",
                result = institucion
            });
        }

        // POST: api/Institucions
        [HttpPost]
        [Route("RegistrarInstitucion")]
        public async Task<IActionResult> guardarInstitucion([FromQuery] string Nombre, [FromQuery] string Logo,
            [FromQuery] string Info)
        {
            var institucion = new Institucion();

            institucion.Nombre = Nombre;
            institucion.Logo = Logo;
            institucion.Info = Info;


            _context.Institucions.Add(institucion);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Institución registrada",
                result = institucion
            });
        }

        // DELETE: api/Institucions/5
        [HttpDelete]
        [Route("EliminarInstitucion")]
        public async Task<IActionResult> BorrarInstitucion(int id)
        {
            var institucion = await _context.Institucions.FindAsync(id);

            if (institucion == null)
            {
                return NotFound();
            }

            _context.Institucions.Remove(institucion);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Institucion eliminada",
                result = institucion
            });
        }

    }
}
