﻿using System;
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
