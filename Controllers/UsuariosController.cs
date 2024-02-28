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
    public class UsuariosController : ControllerBase
    {
        private readonly AppbullyingContext _context;

        public UsuariosController(AppbullyingContext context)
        {
            _context = context;
        }

        // GET: Enlistar todos los usuarios
        [HttpGet]
        [Route("ListarUsuarios")]

        public async Task<ActionResult> listarUsuario()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }

            List<Usuario> usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET: Buscar un usuari por id
        [HttpGet]
        [Route("BuscarUsuario")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // POST: Method to register users
        //ELIMINAR EL ID DE CURSO Y FORMULARIO
        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> guardarUsuario([FromQuery] int Curso, [FromQuery] int Rol, [FromQuery] string Nombre,
            [FromQuery] string NombreUsuario, [FromQuery] string Contrasenia, [FromQuery] int Genero,
            [FromQuery] string Correo)

        {
            var usuario = new Usuario();

            usuario.IdCursoF = Curso;
            usuario.Rol = Rol;
            usuario.Nombre = Nombre;
            usuario.NombreUsuario = NombreUsuario;
            usuario.Contrasenia = Contrasenia;
            usuario.Genero = Genero;
            usuario.Correo = Correo;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Usuario registrado",
                result = usuario
            });
        }

        // PUT: Method to edit user

        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario(int id, [FromQuery] int Curso, [FromQuery] int Mensaje,
            [FromQuery] int Formulario, [FromQuery] int Rol, [FromQuery] string Nombre,
            [FromQuery] string NombreUsuario, [FromQuery] string Contrasenia, [FromQuery] int Genero,
            [FromQuery] string Correo)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del usuario existente con los valores del usuario actualizado
            usuario.IdCursoF = Curso;
            usuario.Rol = Rol;
            usuario.Nombre = Nombre;
            usuario.NombreUsuario = NombreUsuario;
            usuario.Contrasenia = Contrasenia;
            usuario.Genero = Genero;
            usuario.Correo = Correo;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Usuario actualizado",
                result = usuario
            });
        }

        // DELETE: Method to delete users
        [HttpDelete]
        [Route("EliminarUsuario")]
        public async Task<IActionResult> BorrarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Usuario eliminado",
                result = usuario
            });
        }
    }
}
