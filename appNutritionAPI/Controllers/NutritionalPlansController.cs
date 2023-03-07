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
    public class NutritionalPlansController : ControllerBase
    {
        private readonly AppNutritionContext _context;

        public NutritionalPlansController(AppNutritionContext context)
        {
            _context = context;
        }

        // GET: api/NutritionalPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutritionalPlan>>> GetNutritionalPlans()
        {
            return await _context.NutritionalPlans.ToListAsync();
        }

        // GET: api/NutritionalPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NutritionalPlan>> GetNutritionalPlan(int id)
        {
            var nutritionalPlan = await _context.NutritionalPlans.FindAsync(id);

            if (nutritionalPlan == null)
            {
                return NotFound();
            }

            return nutritionalPlan;
        }

        // PUT: api/NutritionalPlans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNutritionalPlan(int id, NutritionalPlan nutritionalPlan)
        {
            if (id != nutritionalPlan.IdPlan)
            {
                return BadRequest();
            }

            _context.Entry(nutritionalPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutritionalPlanExists(id))
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

        // POST: api/NutritionalPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NutritionalPlan>> PostNutritionalPlan(NutritionalPlan nutritionalPlan)
        {
            _context.NutritionalPlans.Add(nutritionalPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNutritionalPlan", new { id = nutritionalPlan.IdPlan }, nutritionalPlan);
        }

        // DELETE: api/NutritionalPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionalPlan(int id)
        {
            var nutritionalPlan = await _context.NutritionalPlans.FindAsync(id);
            if (nutritionalPlan == null)
            {
                return NotFound();
            }

            _context.NutritionalPlans.Remove(nutritionalPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NutritionalPlanExists(int id)
        {
            return _context.NutritionalPlans.Any(e => e.IdPlan == id);
        }
    }
}
