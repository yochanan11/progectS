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
            Meals = new List<Meal>();
        }
        [Key]
        public int ID { get; set; }

        [Display(Name = "שם התוכנית")]
        public string Name { get; set; }

        public User User { get; set; }

        public List<Meal> Meals { get; set; }

        [Display(Name = "תאריך תוכנית")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public void AddMeal(Meal meal) //מוסיף ארוחה לתוכנית ליוזר
        {
            Meals.Add(meal);
            meal.Plane = this;
        }

    }

    
}
