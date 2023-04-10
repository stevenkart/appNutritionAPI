using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appNutritionAPI.Models;
using appNutritionAPI.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace appNutritionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey] // EL APIKEY para seguridad 
    public class ExerciseRoutinesController : ControllerBase
    {
        private readonly AppNutritionContext _context;

        public ExerciseRoutinesController(AppNutritionContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseRoutines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseRoutine>>> GetExerciseRoutines()
        {
            return await _context.ExerciseRoutines.ToListAsync();
        }

        //Este Get permite obtener la info de varios planes de manera filtrada 
        //recibiendo por el IDSTATE de parametro de busqueda
        [HttpGet("GetExcersicePlansFilter")]
        public ActionResult<IEnumerable<ExerciseRoutine>> GetExcersicePlansFilter(int pState)

        {
            //aca usaremos una consulta linq que une informacion de 
            // 3 tablas (user - userRole - UserStatus)
            //Para asignar esos valores al DTO de usuario y entregarlos en formato json

            var query = (from u in _context.ExerciseRoutines
                         where u.IdState == pState
                         select new
                         {
                             u.IdRoutine,
                             u.RoutineName,
                             u.Description,
                             u.ExerciseXample,
                             u.IdState
                         }).ToList();

            //crear un objeto de tipo de DTO de retorno
            List<ExerciseRoutine> list = new List<ExerciseRoutine>();

            foreach (var item in query)
            {
                ExerciseRoutine NewItem = new ExerciseRoutine()
                {
                    IdRoutine = item.IdRoutine,
                    RoutineName = item.RoutineName,
                    Description = item.Description,
                    ExerciseXample = item.ExerciseXample,
                    IdState = item.IdState
                };
                list.Add(NewItem);
            }
            if (list == null)
            {
                return NotFound();
            }
            return list;
        }

        // GET: api/ExerciseRoutines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseRoutine>> GetExerciseRoutine(int id)
        {
            var exerciseRoutine = await _context.ExerciseRoutines.FindAsync(id);

            if (exerciseRoutine == null)
            {
                return NotFound();
            }

            return exerciseRoutine;
        }

        // PUT: api/ExerciseRoutines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseRoutine(int id, ExerciseRoutine exerciseRoutine)
        {
            if (id != exerciseRoutine.IdRoutine)
            {
                return BadRequest();
            }

            _context.Entry(exerciseRoutine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseRoutineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return Ok("Actualizado Correctamente!");
        }


        // PATCH: api/ExerciseRoutine/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchExercise([FromRoute] int id, [FromBody] JsonPatchDocument ExercisenModel)
        {
            var exercise = await _context.ExerciseRoutines.FindAsync(id);

            if (exercise != null)
            {

                ExercisenModel.ApplyTo(exercise);
                await _context.SaveChangesAsync();
                return Ok("Actualizado Correctamente!");
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/ExerciseRoutines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseRoutine>> PostExerciseRoutine(ExerciseRoutine exerciseRoutine)
        {
            _context.ExerciseRoutines.Add(exerciseRoutine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseRoutine", new { id = exerciseRoutine.IdRoutine }, exerciseRoutine);
        }

        // DELETE: api/ExerciseRoutines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseRoutine(int id)
        {
            var exerciseRoutine = await _context.ExerciseRoutines.FindAsync(id);
            if (exerciseRoutine == null)
            {
                return NotFound();
            }

            _context.ExerciseRoutines.Remove(exerciseRoutine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseRoutineExists(int id)
        {
            return _context.ExerciseRoutines.Any(e => e.IdRoutine == id);
        }
    }
}
