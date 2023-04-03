using System;
using System.Collections.Generic;

namespace appNutritionAPI.Models
{
    public partial class Reminder
    {
        //CREATE TABLE Reminders (idReminder int IDENTITY NOT NULL, detail varchar(255) NOT NULL, [date] date NOT NULL, hour time NOT NULL, done bit NOT NULL, idUser int NOT NULL, PRIMARY KEY (idReminder));
        public int IdReminder { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? Hour { get; set; }
        public bool Done { get; set; }
        public int IdUser { get; set; }

        public virtual User? IdUserNavigation { get; set; }
    }
}
