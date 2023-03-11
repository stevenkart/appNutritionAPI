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
using appNutritionAPI.Tools;
using System.Reflection.Metadata.Ecma335;

namespace appNutritionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey] // EL APIKEY para seguridad 
    public class UsersController : ControllerBase
    {
        private readonly AppNutritionContext _context;
        public Crypto MyCrypto { get; set; }
        public UsersController(AppNutritionContext context)
        {
            _context = context;
            MyCrypto = new Tools.Crypto();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("ValidateUserLogin")]
        public async Task<ActionResult<User>> ValidateUserLogin(string pEmail, string pPassword)
        {
           
            string EncriptedPassword = MyCrypto.EncriptarEnUnSentido(pPassword);

            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email == pEmail && e.Password == EncriptedPassword);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        //Ejemplo  GET: /api/Users/GetUserData?Correo=a%40gmail.com
        //Este Get permite obtener la info de un usuario 
        //recibiendo por el Email de parametro de busqueda
        [HttpGet("GetUserData")]
        public ActionResult<IEnumerable<UserDTO>> GetUserData(string pEmail)
        {
            //aca usaremos una consulta linq que une informacion de 
            // 3 tablas (user - userRole - UserStatus)
            //Para asignar esos valores al DTO de usuario y entregarlos en formato json

            var query = (from u in _context.Users
                         join ur in _context.States on u.IdState equals ur.IdState
                         join us in _context.NutritionalPlans on u.IdPlan equals us.IdPlan
                         join ut in _context.ExerciseRoutines on u.IdRoutine equals ut.IdRoutine
                         where u.Email == pEmail && u.IdState != 2
                         select new
                         {
                             IdUser = u.IdUser,
                             FullName = u.FullName,
                             Email = u.Email,
                             Phone = u.Phone,
                             Password = u.Password,
                             Weight = u.Weight,
                             Hight = u.Hight,
                             Age = u.Age,
                             FatPercent = u.FatPercent,
                             Genre = u.Genre,
                             IdState = ur.IdState,
                             StateDescription = ur.StateName,
                             IdPlan = us.IdPlan,
                             NutritionalPlanDescription = us.Description,
                             IdRoutine = ut.IdRoutine,
                             ExerciseRoutineDescription = ut.Description,

                         }).ToList();

            //crear un objeto de tipo de DTO de retorno
            List<UserDTO> list = new List<UserDTO>();

            foreach (var item in query)
            {
                UserDTO NewItem = new UserDTO()
                {
                    IdUser = item.IdUser,
                    FullName = item.FullName,
                    Email = item.Email,
                    Phone = item.Phone,
                    Password = item.Password,
                    Weight = item.Weight,
                    Hight = item.Hight,
                    Age = item.Age,
                    FatPercent = item.FatPercent,
                    Genre = item.Genre,
                    IdState = item.IdState,
                    StateDescription = item.StateDescription,
                    IdPlan = item.IdPlan,
                    NutritionalPlanDescription = item.NutritionalPlanDescription,
                    IdRoutine = item.IdRoutine,
                    ExerciseRoutineDescription = item.ExerciseRoutineDescription,
                };
                list.Add(NewItem);
            }
            if (list == null)
            {
                return NotFound();
            }
            return list;
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            string EncriptedPassword = MyCrypto.EncriptarEnUnSentido(user.Password);

            user.Password = EncriptedPassword;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
