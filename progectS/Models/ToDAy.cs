using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class ToDAy
    {
        public ToDAy()
        {
            MealsToDay = new List<MealToDAy>();
        }
        [Key]
        public int ID { get; set; }

        [Display(Name = "שם המשתמש")]
        public User User { get; set; }

        [Display(Name = "תאריך שבו אכלתי")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "מצב הרוח")]
        public string Mood { get; set; }

        public List<MealToDAy> MealsToDay { get; set; }

        public void AddFood(Food food,TypeOfMeal type,double quantity)
        {
            MealToDAy mealTo = MealsToDay.Find(f => f.MealNameToDay == type);
            if (mealTo == null) 
            { 
                mealTo = new MealToDAy { MealNameToDay = type, ToDAy = this };
                MealsToDay.Add(mealTo);
            }
            mealTo.Foods.Add(new TodayFoodInMeal { Food = food, Quantity = quantity });

        }
       

    }
}
