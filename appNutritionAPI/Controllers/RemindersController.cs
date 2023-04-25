using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appNutritionAPI.Models;
using appNutritionAPI.Attributes;
using appNutritionAPI.ModelsDTOs;
using System.Reflection.PortableExecutable;

using Microsoft.AspNetCore.JsonPatch;
using appNutritionAPI.Tools;

namespace appNutritionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey] // EL APIKEY para seguridad 
    public class RemindersController : ControllerBase
    {
        private readonly AppNutritionContext _context;

        public RemindersController(AppNutritionContext context)
        {
            _context = context;
        }

        // GET: api/Reminders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reminder>>> GetReminders()
        {
            return await _context.Reminders.ToListAsync();
        }

        // GET: api/Reminders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reminder>> GetReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);

            return reminder == null ? NotFound() : Ok(reminder);
        }

        // GET: api/Reminders/5
        [HttpGet("GetReminderByUserId")]
        public ActionResult<IEnumerable<Reminder>> GetReminderByUserId(int pUserId)
        {
            //SingleOrDefaultAsync(e => e.IdUser == pId);
            //return await _context.Reminders.; 

            var query = (from r in _context.Reminders
                         where r.IdUser == pUserId && r.Done == false
                         select new
                         {
                             r.IdReminder,
                             r.Detail,
                             r.Date,
                             r.Hour
                         }).ToList();

            //crear un objeto de tipo de DTO de retorno
            List<Reminder> list = new List<Reminder>();

            foreach (var item in query)
            {
                Reminder NewItem = new Reminder()
                {
                    IdReminder = item.IdReminder,
                    Detail = item.Detail,
                    Date = item.Date,
                    Hour = item.Hour,
                };
                list.Add(NewItem);
            }

            return list == null ? NotFound() : Ok(list);

        }

        // PUT: api/Reminders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminder(int id, Reminder reminder)
        {
            if (id != reminder.IdReminder)
            {
                return BadRequest();
            }

            _context.Entry(reminder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(id))
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

        // PATCH: api/Reminder/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // NuGet package Microsoft.AspNetCore.JsonPatch
        // https://learn.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-7.0
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser([FromRoute] int id, [FromBody] JsonPatchDocument<Reminder> ReminderModel)
        {

            var reminder = await _context.Reminders.FindAsync(id);

            if (reminder != null)
            {
                ReminderModel.ApplyTo(reminder);
                await _context.SaveChangesAsync();
                return Ok("Actualizado Correctamente!");
            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/Reminders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reminder>> PostReminder(Reminder reminder)
        {
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminder", new { id = reminder.IdReminder }, reminder);
        }

        // DELETE: api/Reminders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReminderExists(int id)
        {
            return _context.Reminders.Any(e => e.IdReminder == id);
        }
    }
}
