using appNutritionAPI.Models;

namespace appNutritionAPI.ModelsDTOs
{
    public class UserDTO
    {
        //Un DTO (Data Transfer Object) sirve para;
        //1. Para que el equipor de desarrollo de los front ends (app en este caso)
        //no entiendan la estrutura real de la tabla a nivel de la base de datos.

        //2. Simplificar objetos complejos en estructuras mas simples para que los 
        //json resultantes sean mucho mas faciles de gestionar

        //3. En caso en los que se deba regenerar los modelos por medio de 
        //scaffold -f los controles sigan trabajando con normalidad

        //en este he decidido escribir los nombres en espanol solo por ejemplo de uso de los DTOs

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNum { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public decimal? Code { get; set; }
        public decimal W { get; set; }
        public decimal H { get; set; }
        public int Ages { get; set; }
        public decimal Fat { get; set; }
        public string Genres { get; set; } = null!;
        public int IdStates { get; set; }
        public int? IdPlans { get; set; }
        public int? IdRoutines { get; set; }

    }
}
