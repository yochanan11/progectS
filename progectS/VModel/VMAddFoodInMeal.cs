using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;

namespace progectS.VModel
{
    public class VMAddFoodInMeal
    {
        public Meal Meal { get; set; }

        public int MealID { get; set; }

        public FoodInMeal FoodInMeal { get; set; }

        public int FoodID { get; set; }

        public List<Food> FromApi { get; set; }
    }
}
