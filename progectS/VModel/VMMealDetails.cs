using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;

namespace progectS.VModel
{
    public class VMMealDetails
    {
        public Meal Meal { get; set; }

        public int MealID { get; set; }

        public string MealName { get; set; }

        public FoodInMeal Food { get; set; }
    }
}
