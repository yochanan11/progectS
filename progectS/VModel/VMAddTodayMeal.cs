using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;

namespace progectS.VModel
{
    public class VMAddTodayMeal
    {
        public VMAddTodayMeal()
        {

        }
        public Day Day { get; set; }

        public int DayID { get; set; }

        public User User { get; set; }

        public int UserID { get; set; }

        public MealToDAy MealToDAy { get; set; }

        public List<TypeOfMeal> TypeOfMeals { get; set; }

        public int TypeId { get; set; }
    }
}
