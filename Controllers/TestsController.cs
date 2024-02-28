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
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly AppbullyingContext _context;

        public TestsController(AppbullyingContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet]
        [Route("ListarTest")]
        public async Task<IActionResult> listarTests()
        {

            List<Test> tests = await _context.Tests.ToListAsync();


            return Ok(tests);

        }

        // GET: api/Tests/5
        [HttpGet]
        [Route("BuscarTest")]
        public async Task<IActionResult> ObtenerTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        // PUT: api/Tests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("RegistrarTest")]
        public async Task<IActionResult> guardarTest([FromQuery] string NombreTest)
        {
            var test = new Test();

            test.NombreTest = NombreTest;
   

            _context.Tests.Add(test);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Mensaje registrado",
                result = test
            });
        }


        // DELETE: api/Tests/5
        [HttpDelete]
        [Route("EliminarTest")]
        public async Task<IActionResult> BorrarTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Mensaje actualizado",
                result = test
            });
        }

        private bool TestExists(int id)
        {
            return (_context.Tests?.Any(e => e.IdTest == id)).GetValueOrDefault();
        }
    }
}
