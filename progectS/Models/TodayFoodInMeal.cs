using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class TodayFoodInMeal
    {
        [Key]
        public int ID { get; set; }

        public MealToDAy MealToDAy{ get; set; }

        public Food Food { get; set; }

        public double Quantity { get; set; }//כמות
    }
}
