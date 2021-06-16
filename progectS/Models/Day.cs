using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class Day
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "שבוע")]
        public Plane Plane { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "שם יום")]
        [DataType(DataType.Date)]
        public string DayName { get; set; }

        public List<Meal> Meals { get; set; }

        public void AddMeal(Meal meal) //מוסיף ארוחה לתוכנית ליוזר
        {
            Meals.Add(meal);
            meal.Day = this;
        }
    }
}
