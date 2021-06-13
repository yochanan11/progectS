using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class FoodInMeal
    {
        public FoodInMeal()
        {
            
        }
        [Key]
        public int ID { get; set; }

        public Meal Meal { get; set; }

        public Food Food { get; set; }//איזה אוכל יש בתוך הארוחה

        public double Quantity { get; set; }//כמות

        public Plane Plane { get; set; }

        

    }
}
