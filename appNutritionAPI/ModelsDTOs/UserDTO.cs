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
        public string NombreCompleto { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public decimal? Code { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public int Edad { get; set; }
        public decimal Grasa { get; set; }
        public string Genero { get; set; } = null!;
        public int estadosID { get; set; }
        public int? planID { get; set; }
        public int? RutinaID { get; set; }

    }
}
