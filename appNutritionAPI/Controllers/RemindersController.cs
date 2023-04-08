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

            if (reminder == null)
            {
                return NotFound();
            }

            return reminder;
        }

        // GET: api/Reminders/5
        [HttpGet("GetReminderByUserId")]
        public ActionResult<IEnumerable<Reminder>> GetReminderByUserId(int pUserId)
        {
            //SingleOrDefaultAsync(e => e.IdUser == pId);
            //return await _context.Reminders.; 

            var query = (from r in _context.Reminders
                         where r.IdUser == pUserId
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

        /*
         {
            "idReminder": 0,
            "detail": "detaller",
            "date": "2023-12-30T00:00:21.044Z",
            "hour": "23:59:59",
            "done": true,
            "idUser": 7
         }
         */
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
