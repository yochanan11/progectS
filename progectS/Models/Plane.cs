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

     /*   public List<Meal> Meals { get; set; }*/

        public List<Day> Days { get; set; }

        [Display(Name = "תאריך תוכנית")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /*public void AddMeal(Meal meal) //מוסיף ארוחה לתוכנית ליוזר
        {
            Meals.Add(meal);
            meal.Day = this;
        }*/
        public void AddDays()
        {
            Day day1 = new Day { DayName = "יום ראשון", Date = Date, Plane = this };
            Day day2 = new Day { DayName = "יום שני", Date = Date.AddDays(1), Plane = this };
            Day day3 = new Day { DayName = "יום שלישי", Date = Date.AddDays(2), Plane = this };
            Day day4 = new Day { DayName = "יום רביעי", Date = Date.AddDays(3), Plane = this };
            Day day5 = new Day { DayName = "יום חמישי", Date = Date.AddDays(4), Plane = this };
            Day day6 = new Day { DayName = "יום שישי", Date = Date.AddDays(5), Plane = this };
            Day day7 = new Day { DayName = "יום שבת", Date = Date.AddDays(6), Plane = this };
            Days = new List<Day>();
            Days.Add(day1);
            Days.Add(day2);
            Days.Add(day3);
            Days.Add(day4);
            Days.Add(day5);
            Days.Add(day6);
            Days.Add(day7);
        }
    }

    
}
