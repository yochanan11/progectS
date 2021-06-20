using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class Day
    {
        public Day()
        {
            Meals = new List<Meal>();
            MealsToDAy = new List<MealToDAy>();
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "שבוע")]
        public Plane Plane { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "שם יום")]
        public string DayName { get; set; }

        public List<Meal> Meals { get; set; }

        public List<MealToDAy> MealsToDAy { get; set; }

        public void AddMeal(Meal meal) //מוסיף ארוחה לתוכנית ליוזר
        {
            Meals.Add(meal);
            meal.Day = this;
        }

        public void AddMealToday(MealToDAy meal) //מוסיף ארוחה לתוכנית ליוזר
        {
            MealsToDAy.Add(meal);
            meal.ToDay = this;
        }

        public SumALLProperties SumALLProperties
        {
            get
            {
                SumALLProperties Sum = new SumALLProperties(0, 0, 0);
                foreach (Meal Meal in Meals)
                {
                    Sum.SumCaloris += Meal.SumCalories;
                    Sum.SumCarbohydrates  += Meal.SumCarbohydrates;
                    Sum.SumProteins += Meal.SumProteins;
                }
                return Sum;
            }
        }
        public SumALLProperties SumALLPropertiesToDay
        {
            get
            {
                SumALLProperties Sum = new SumALLProperties(0, 0, 0);
                foreach (MealToDAy Meal in MealsToDAy)
                {
                    Sum.SumCaloris += Meal.SumCaloriesT;
                    Sum.SumCarbohydrates += Meal.SumCarbohydratesT;
                    Sum.SumProteins += Meal.SumProteinsT;
                }
                return Sum;
            }
        }
    }
}
