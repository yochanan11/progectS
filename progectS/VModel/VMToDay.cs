using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;

namespace progectS.VModel
{
    public class VMToDay
    {

        public Day Day { get; set; }

        public User User { get; set; }

        public ToDAy ToDAy { get; set; }

        public MealToDAy MealToDAy { get; set; }

        public TodayFoodInMeal TodayFoodsInMeal { get; set; }
        public List<TypeOfMeal> type { get; set; }
        public int TypeId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
        public int FoodId { get; set; }

        /*public Day GetDay
        {
            get
            {
                return Plane.Days.Find(d => d.Date == Date);
            }
        }*/
        
    }
}
