using System;
using System.Collections.Generic;

namespace appNutritionAPI.Models
{
    public partial class Reminder
    {
        public int IdReminder { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public bool Done { get; set; }
        public int IdUser { get; set; }

        public virtual User? IdUserNavigation { get; set; }
    }
}
