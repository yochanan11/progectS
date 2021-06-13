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
            Date = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }

        public Plane Plane { get; set; }//לאיזה יעד הארוחה שייכת

        [Display(Name = "שם הארוחה")]
        public TypeOfMeal MealName { get; set; }// שם הארוחה

        [Display(Name = "תאריך")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }// תאריך

        [Display(Name = "רשימת האוכל בתוך הארוחה")]
        public List<FoodInMeal> Foods { get; set; }//רשימת האוכל בתוך הארוחה
        [Display(Name = "סה''כ קלוריות")]
        public int SumCalories//סה"כ קלוריות
        {
            get
            {
                double Sum = 0;
                foreach (FoodInMeal food in Foods)
                {
                    if (food.Quantity == 0 || food.Food.Caloris == 0) return (int)Sum;
                    Sum += food.Food.Caloris*food.Quantity;
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
}
