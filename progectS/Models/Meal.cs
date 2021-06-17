using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class Meal
    {
        public Meal()
        { 
            Foods = new List<FoodInMeal>(); 
        }
        [Key]
        public int ID { get; set; }

        public Day Day { get; set; }//לאיזה יום הארוחה שייכת

        [Display(Name = "שם הארוחה")]
        public TypeOfMeal MealName { get; set; }// שם הארוחה

       
        [Display(Name = "רשימת האוכל בתוך הארוחה")]
        public List<FoodInMeal> Foods { get; set; }//רשימת האוכל בתוך הארוחה

        public SumALLProperties SumALLProperties
        {
            get
            {
                return new SumALLProperties(SumCalories, SumCarbohydrates, SumProteins);
            }
        }

        [Display(Name = "סה''כ קלוריות")]
        public int SumCalories//סה"כ קלוריות
        {
            get
            {
                double Sum = 0;
                foreach (FoodInMeal food in Foods)
                {
                    if (food.Quantity > 0 || food.Food.Caloris > 0) 
                    Sum += food.Food.Caloris*food.Quantity;
                }
                return (int)Sum;
            }
        }
        [Display(Name = "סה''כ חלבונים")]
        public int SumProteins//סה"כ חלבונים
        {
            get
            {
                double Sum = 0;
                foreach (FoodInMeal food in Foods)
                {
                    if (food.Quantity > 0 || food.Food.Proteins> 0)
                    Sum += food.Food.Proteins * food.Quantity;
                }
                return (int)Sum;
            }
        }
        [Display(Name = "סה''כ פחמימות")]
        public int SumCarbohydrates//סה"כ פחמימות
        {
            get
            {
                double Sum = 0;
                foreach (FoodInMeal food in Foods)
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
                AddFood(food, 1);
            }
        }
        public void AddFood(Food food, double? quantity) // להוסיף מאכל בתוך הארוחה אם רוצים אפשר להכניס גם כמות לא חובה
        {
            double myQuantity = 1;
            if (quantity != null)
            {
                myQuantity = quantity.Value;
            }
            Foods.Add(new FoodInMeal() { Food = food, Meal = this, Quantity = myQuantity });
        }
       

    }
    public class SumALLProperties
    {
        public SumALLProperties(int SumC, int SumCh, int SumP)
        {
            SumCaloris = SumC; SumCarbohydrates = SumCh; SumProteins = SumP;
        }
        public int SumCaloris;

        public int SumCarbohydrates;

        public int SumProteins;
    }
}
