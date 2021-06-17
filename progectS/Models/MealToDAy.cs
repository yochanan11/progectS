using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class MealToDAy
    {
        public MealToDAy()
        {
            Foods = new List<TodayFoodInMeal>();
        }
        [Key]
        public int ID { get; set; }

        public ToDAy ToDAy { get; set; }

        public TypeOfMeal MealNameToDay { get; set; }

        public List<TodayFoodInMeal> Foods { get; set; }
    }
}
