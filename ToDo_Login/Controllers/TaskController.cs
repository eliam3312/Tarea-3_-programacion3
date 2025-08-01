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


         // Obtener tarea por ID
        [HttpGet("{id}")]
        public IActionResult ObtenerTarea(int id)
        {
            var tarea = dbContext.Tareas.Find(id);

            if (tarea == null)
            {
                return NotFound(new { success = false, message = "Tarea no encontrada." });
            }

            return Ok(tarea);
        }

        // Actualizar tarea
        [HttpPut("{id}")]
        public IActionResult ActualizarTarea(int id, TareaDTO tareaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tarea = dbContext.Tareas.Find(id);

            if (tarea == null)
            {
                return NotFound(new { success = false, message = "Tarea no encontrada." });
            }

            tarea.title = tareaDTO.title;
            tarea.description = tareaDTO.description;
            tarea.status = tareaDTO.status;
            tarea.updated_at = DateTime.UtcNow;

            dbContext.SaveChanges();

            return Ok(new { success = true, message = "Tarea actualizada exitosamente." });
        }



    }
}