using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appNutritionAPI.Models;
using appNutritionAPI.Tools;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections;
using appNutritionAPI.ModelsDTOs;
using appNutritionAPI.Attributes;

namespace appNutritionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey] // EL APIKEY para seguridad 
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


        //Este Get permite obtener la info de varios planes de manera filtrada 
        //recibiendo por el IDSTATE de parametro de busqueda
        [HttpGet("GetNutritionalPlansFilter")]
        public ActionResult<IEnumerable<NutritionalPlan>> GetNutritionalPlansFilter(int pState)

        {
            //aca usaremos una consulta linq que une informacion de 
            // 3 tablas (user - userRole - UserStatus)
            //Para asignar esos valores al DTO de usuario y entregarlos en formato json

            var query = (from u in _context.NutritionalPlans
                         where u.IdState == pState
                         select new
                         {
                             u.IdPlan,
                             u.Name,
                             u.Description,
                             u.PlanXample,
                             u.IdState
                         }).ToList();

            //crear un objeto de tipo de DTO de retorno
            List<NutritionalPlan> list = new List<NutritionalPlan>();

            foreach (var item in query)
            {
                NutritionalPlan NewItem = new NutritionalPlan()
                {
                    IdPlan = item.IdPlan,
                    Name = item.Name,
                    Description = item.Description,
                    PlanXample = item.PlanXample,
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

        [HttpGet("GetNutritionalPlansFilterId")]
        public ActionResult<IEnumerable<NutritionalPlan>> GetNutritionalPlansFilterId(int pID)

        {
            //aca usaremos una consulta linq que une informacion de 
            // 3 tablas (user - userRole - UserStatus)
            //Para asignar esos valores al DTO de usuario y entregarlos en formato json

            var query = (from u in _context.NutritionalPlans
                         where u.IdPlan == pID
                         select new
                         {
                             u.IdPlan,
                             u.Name,
                             u.Description,
                             u.PlanXample,
                             u.IdState
                         }).ToList();

            //crear un objeto de tipo de DTO de retorno
            List<NutritionalPlan> list = new List<NutritionalPlan>();

            foreach (var item in query)
            {
                NutritionalPlan NewItem = new NutritionalPlan()
                {
                    IdPlan = item.IdPlan,
                    Name = item.Name,
                    Description = item.Description,
                    PlanXample = item.PlanXample,
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

            //return NoContent();
            return Ok("Actualizado Correctamente!");
        }


        // PATCH: api/NutritionalPlans/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPlan([FromRoute] int id, [FromBody] JsonPatchDocument PlanModel)
        {
            var plan = await _context.NutritionalPlans.FindAsync(id);

            if (plan != null)
            {

                PlanModel.ApplyTo(plan);
                await _context.SaveChangesAsync();
                return Ok("Actualizado Correctamente!");
            }
            else
            {
                return NotFound();
            }
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

            return Ok("Eliminado Correctamente!");
        }

        private bool NutritionalPlanExists(int id)
        {
            return _context.NutritionalPlans.Any(e => e.IdPlan == id);
        }
    }
}
