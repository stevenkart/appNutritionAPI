using System;
using System.Collections.Generic;

namespace appNutritionAPI.Models
{
    public partial class User
    {
        public User()
        {
            Reminders = new HashSet<Reminder>();
        }

        public int IdUser { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? RecoveryCode { get; set; }
        public decimal Weight { get; set; }
        public decimal Hight { get; set; }
        public int Age { get; set; }
        public decimal FatPercent { get; set; }
        public string Genre { get; set; } = null!;
        public int IdState { get; set; }
        public int? IdPlan { get; set; }
        public int? IdRoutine { get; set; }

        public virtual NutritionalPlan? IdPlanNavigation { get; set; }
        public virtual ExerciseRoutine? IdRoutineNavigation { get; set; }
        public virtual State? IdStateNavigation { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
