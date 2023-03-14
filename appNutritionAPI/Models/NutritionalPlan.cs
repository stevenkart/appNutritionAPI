using System;
using System.Collections.Generic;

namespace appNutritionAPI.Models
{
    public partial class NutritionalPlan
    {
        public NutritionalPlan()
        {
            Users = new HashSet<User>();
        }

        public int IdPlan { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PlanXample { get; set; }
        public int IdState { get; set; }

        public virtual State? IdStateNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
