using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo_Login.Models;

namespace ToDo_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public UsuariosController(MyDbContext dbContext)
        {

            this.dbContext = dbContext;

        }
        [HttpPost]
        [Route("Registro")]
        public IActionResult Registro(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Datos de modelo inválidos", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }

            try
            {
                var objUser = dbContext.Usuarios.FirstOrDefault(x => x.email == userDTO.email);
                if (objUser == null)
                {
                    dbContext.Usuarios.Add(new Usuario
                    {
                        first_name = userDTO.first_name,
                        last_name = userDTO.last_name,
                        email = userDTO.email,
                        password = userDTO.password
                    });
                    dbContext.SaveChanges();
                    return Ok(new { success = true, message = "Usuario registrado de forma exitosa" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Correo electrónico ya está en uso." });
                }
            }
            catch (Exception ex)
            {
                // Registra el error (opcional)
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Datos de modelo inválidos", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }

            try
            {
                var usuario = dbContext.Usuarios.FirstOrDefault(x => x.email == loginDTO.email && x.password == loginDTO.password);

                if (usuario != null)
                {
                    if (usuario.IsConfirmed)
                    {
                        return Ok(new { success = true, message = "Inicio de sesión exitoso", user = usuario });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Cuenta no Verificada. Por favor, confirma tu cuenta." });
                    }
                }

                return NotFound(new { success = false, message = "Correo electrónico o contraseña incorrectos." });
            }
            catch (Exception ex)
            {
                // Registra el error (opcional)
                return StatusCode(500, new { success = false, message = "Error interno del servidor." });
            }
        }





        [HttpGet]
        [Route("ObtenerUsuario")]
        public IActionResult ObtenerUsuario()
        {
            return Ok(dbContext.Usuarios.ToList());
        
        }



        [HttpGet]
        [Route("ObtenerUsuario_id")]
        public IActionResult ObtenerUsuario(int id)
        {
            var user = dbContext.Usuarios.FirstOrDefault(x => x.Id == id);
            if (user != null)
                return Ok(user);
            else
                return NoContent();
        }
    }
}
