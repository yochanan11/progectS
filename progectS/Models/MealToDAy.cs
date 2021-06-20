using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class MealToDAy
    {
        public MealToDAy()
        {
            Foods = new List<TodayFoodInMeal>();
        }
        [Key]
        public int ID { get; set; }

        public Day ToDay { get; set; }

        public TypeOfMeal MealNameToDay { get; set; }


        public List<TodayFoodInMeal> Foods { get; set; }

        public SumALLProperties SumALLPropertiesToday
        {
            get
            {
                return new SumALLProperties(SumCaloriesT, SumCarbohydratesT, SumProteinsT);
            }
        }

        [Display(Name = "סה''כ קלוריות")]
        public int SumCaloriesT//סה"כ קלוריות
        {
            get
            {
                double Sum = 0;
                foreach (TodayFoodInMeal food in Foods)
                {
                    if (food.Quantity > 0 || food.Food.Caloris > 0)
                        Sum += food.Food.Caloris * food.Quantity;
                }
                return (int)Sum;
            }
        }
        [Display(Name = "סה''כ חלבונים")]
        public int SumProteinsT//סה"כ חלבונים
        {
            get
            {
                double Sum = 0;
                foreach (TodayFoodInMeal food in Foods)
                {
                    if (food.Quantity > 0 || food.Food.Proteins > 0)
                        Sum += food.Food.Proteins * food.Quantity;
                }
                return (int)Sum;
            }
        }
        [Display(Name = "סה''כ פחמימות")]
        public int SumCarbohydratesT//סה"כ פחמימות
        {
            get
            {
                double Sum = 0;
                foreach (TodayFoodInMeal food in Foods)
                {
                    if (food.Quantity > 0 || food.Food.Carbohydrates > 0)
                        Sum += food.Food.Carbohydrates * food.Quantity;
                }
                return (int)Sum;
            }
        }

        public void AddFoods(List<Food> foods) //להוסיף אוכל בתוך ארוחה
        {
            foreach (Food food in foods)
            {
                AddFood(food, 1,"");
            }
        }
        public void AddFood(Food food, double? quantity,string mood) // להוסיף מאכל בתוך הארוחה אם רוצים אפשר להכניס גם כמות לא חובה
        {
            double myQuantity = 1;
            if (quantity != null)
            {
                myQuantity = quantity.Value;
            }
            Foods.Add(new TodayFoodInMeal() { Food = food,  MealToDAy = this, Quantity = myQuantity, Mood = mood });
        }


    }
}
