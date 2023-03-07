using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appNutritionAPI.Models;

namespace appNutritionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            return NoContent();
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
