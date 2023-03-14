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
                         //join ur in _context.States on u.IdState equals ur.IdState
                         //join us in _context.NutritionalPlans on u.IdPlan equals us.IdPlan
                         //join ut in _context.ExerciseRoutines on u.IdRoutine equals ut.IdRoutine
                         where u.Email == pEmail && u.IdState == 1
                         select new
                         {
                             u.IdUser,
                             u.FullName,
                             u.Email,
                             u.Phone,
                             u.Password,
                             u.Weight,
                             u.Hight,
                             u.Age,
                             u.FatPercent,
                             u.Genre,
                             u.IdState,
                             u.IdPlan,
                             u.IdRoutine,

                         }).ToList();

            //crear un objeto de tipo de DTO de retorno
            List<UserDTO> list = new List<UserDTO>();

            foreach (var item in query)
            {
                UserDTO NewItem = new UserDTO()
                {
                    Id = item.IdUser,
                    NombreCompleto = item.FullName,
                    Correo = item.Email,
                    Tel = item.Phone,
                    //Pass = item.Password,
                    Peso = item.Weight,
                    Altura = item.Hight,
                    Edad = item.Age,
                    Grasa = item.FatPercent,
                    Genero = item.Genre,
                    estadosID = item.IdState,
                    planID = item.IdPlan,
                    RutinaID = item.IdRoutine  
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
        public async Task<IActionResult> PutUser(int id, User user) // el metodo lleva en el json el ID del usuario y tambien como params Id
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                string EncriptedPassword = MyCrypto.EncriptarEnUnSentido(user.Password);
                user.Password = EncriptedPassword;
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

            return Ok("Actualizado Correctamente!");
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

            //return NoContent();
            return Ok("Eliminado Correctamente!");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
