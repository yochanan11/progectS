using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class Plane
    {
        public Plane()
        {
            Date = DateTime.Now; 
        }
        [Key]
        public int ID { get; set; }

        [Display(Name = "שם התוכנית")]
        public string Name { get; set; }

        public User User { get; set; }


        public List<Day> Days { get; set; }

        [Display(Name = "תאריך תוכנית")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        
        public void AddDays()
        {
            string[] Darry = { "יום הראשון", "יום השני", "יום השלישי", "יום הרביעי", "יום החמישי", "יום השישי", "יום השביעי" };
            Days = new List<Day>();
            for (int i = 0; i < Darry.Length; i++)
            {
                Day day = new Day { DayName = Darry[i], Date = Date.AddDays(i), Plane = this };
                Days.Add(day);
            }
         
        }
    }

    
}
