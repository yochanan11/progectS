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

        [Display(Name = "לאיזה ארוחה שייך")]
        public MealToDAy MealToDAy{ get; set; }
        [Display(Name = "שם המאכל")]
        public Food Food { get; set; }

        [Display(Name = "מצב הרוח")]
        public string Mood { get; set; }

        [Display(Name = "כמות")]
        public double Quantity { get; set; }//כמות
    }
}
