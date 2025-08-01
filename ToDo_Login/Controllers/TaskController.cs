// Controllers/TareasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo_Login.Models;
using System;
using System.Linq;﻿

namespace ToDo_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly MyDbContext dbContext;

        public TareasController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Crear tarea
        [HttpPost]
        public IActionResult CrearTarea(TareaDTO tareaDTO, int userId) // Agrega userId como parámetro
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el usuario existe
            var usuario = dbContext.Usuarios.Find(userId);
            if (usuario == null)
            {
                return BadRequest(new { success = false, message = "ID de usuario no válido." });
            }

            var tarea = new Tarea
            {
                user_id = userId, // Asignar el ID del usuario a la tarea
                title = tareaDTO.title,
                description = tareaDTO.description,
                status = tareaDTO.status,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            dbContext.Tareas.Add(tarea);
            dbContext.SaveChanges();

            return Ok(new { success = true, message = "Tarea creada exitosamente." });
        }



    }
}