using progectS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.VModel
{
    public class VMToAddFoodInMeal
    {
        public MealToDAy Meal { get; set; }

        public int MealID { get; set; }

        public TodayFoodInMeal FoodInMeal { get; set; }

        public int FoodID { get; set; }
    }
}
