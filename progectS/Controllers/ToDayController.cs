using Microsoft.AspNetCore.Mvc;
using progectS.Models;
using progectS.VModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace progectS.Controllers
{
    public class ToDayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMealToDay(int? id)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction("Connect","user");
            VMAddTodayMeal VM = new VMAddTodayMeal
            {
                User = DAL.Get.User,
                UserID = DAL.Get.User.ID,
                TypeOfMeals = DAL.Get.TypeOfMeals.ToList(),
                MealToDAy = new MealToDAy(),
                Day = DAL.Get.Days.ToList().Find(D => D.ID == id),
                DayID = DAL.Get.Days.ToList().Find(D => D.ID == id).ID

            };
            return View(VM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMealToDay(VMAddTodayMeal VM)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction("Connect", "User");
            User user = DAL.Get.User;
            Day day = DAL.Get.Days.ToList().Find(d => d.ID == VM.DayID);
            TypeOfMeal type = DAL.Get.TypeOfMeals.ToList().Find(t => t.Id == VM.TypeId);
            MealToDAy meal = new MealToDAy
            {
                MealNameToDay = type,
                ToDay = day,
            };
            DAL.Get.Days.ToList().Find(d => d.ID == day.ID).AddMealToday(meal);
            DAL.Get.SaveChanges();

            return RedirectToAction("DaylDetails","plane", new { id = day.ID });
        }
        public IActionResult ToMealDetails(int? id)
        {

            MealToDAy meal = DAL.Get.MealsToDAy.Include(f => f.Foods).Include(m => m.MealNameToDay).ToList().Find(m => m.ID == id);
            VMToMealDetails VM = new VMToMealDetails
            {
                Food = DAL.Get.TodayFoodsInMeal.Include(f => f.Food).ToList().Find(f => f.MealToDAy.ID == meal.ID),
                Meal = DAL.Get.MealsToDAy.ToList().Find(m => m.ID == meal.ID),
                MealID = meal.ID

            };
            return View(VM);
        }
        public IActionResult AddFoodsInMealToDay(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));
            MealToDAy meal = DAL.Get.MealsToDAy.ToList().Find(m => m.ID == id);
            VMToAddFoodInMeal VM = new VMToAddFoodInMeal
            {
                Meal = DAL.Get.MealsToDAy.ToList().Find(m => m.ID == id),
                MealID = DAL.Get.MealsToDAy.ToList().Find(m => m.ID == id).ID,

                FoodInMeal = new TodayFoodInMeal
                {
                    MealToDAy = meal
                }
            };
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFoodsInMealToDay(VMToAddFoodInMeal VMfood)
        {
            MealToDAy meal = DAL.Get.MealsToDAy.ToList().Find(m => m.ID == VMfood.MealID);
            Food food = DAL.Get.Foods.ToList().Find(f => f.ID == VMfood.FoodID);
            DAL.Get.MealsToDAy.ToList().Find(m => m.ID == VMfood.MealID).AddFood(food, VMfood.FoodInMeal.Quantity,VMfood.FoodInMeal.Mood);
            DAL.Get.SaveChanges();
            return RedirectToAction(nameof(ToMealDetails), new { id = meal.ID });
        }

    }
}
