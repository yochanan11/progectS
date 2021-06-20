using progectS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.VModel
{
    public class VMToMealDetails
    {
        
            public MealToDAy Meal { get; set; }

            public int MealID { get; set; }

            public string MealName { get; set; }

            public TodayFoodInMeal Food { get; set; }
        
    }
}
