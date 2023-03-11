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

        public int IdUser { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? RecoveryCode { get; set; }
        public decimal Weight { get; set; }
        public decimal Hight { get; set; }
        public int Age { get; set; }
        public decimal FatPercent { get; set; }
        public string Genre { get; set; } = null!;
        public int IdState { get; set; }
        public int? IdPlan { get; set; }
        public int? IdRoutine { get; set; }
        public string? NutritionalPlanDescription { get; set; }
        public string? ExerciseRoutineDescription { get; set; }
        public string? StateDescription  { get; set; }

    }
}
