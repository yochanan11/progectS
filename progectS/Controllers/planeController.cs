using Microsoft.AspNetCore.Mvc;
using progectS.Models;
using progectS.VModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Helpers;

namespace progectS.Controllers
{
    public class planeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null) return RedirectToAction(nameof(ShowPlanes), new { id = DAL.Get.User.ID });
            Plane plane = DAL.Get.Planes.ToList().Find(p => p.ID == id);
            if (plane == null) return RedirectToAction(nameof(ShowPlanes), new { id = DAL.Get.User.ID });
            return View(plane);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Plane plane)
        {
            Plane plane1 = DAL.Get.Planes.ToList().Find(p => p.ID == plane.ID);
            plane1.Name = plane.Name;
            plane1.Date = plane.Date;
            DAL.Get.SaveChanges();
            return RedirectToAction(nameof(ShowPlanes), new { id = DAL.Get.User.ID });
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(ShowPlanes),new { id = DAL.Get.User.ID });
            Plane plane = DAL.Get.Planes.ToList().Find(p => p.ID == id);
            if (plane == null) return RedirectToAction(nameof(ShowPlanes), new { id = DAL.Get.User.ID });
            return View(plane);
        }
        [HttpPost]
        public IActionResult Delete(Plane plane)
        {
            if (plane == null) return RedirectToAction(nameof(ShowPlanes), new { id = DAL.Get.User.ID });
            Plane plane1 = DAL.Get.Planes.Include(d=> d.Days).ToList().Find(p => p.ID == plane.ID);
            if (plane == null) return RedirectToAction(nameof(ShowPlanes), new { id = DAL.Get.User.ID });
            DAL.Get.Days.RemoveRange(plane1.Days);
            DAL.Get.Planes.Remove(plane1);
            return RedirectToAction(nameof(ShowPlanes), new { id = DAL.Get.User.ID });
        }

        public IActionResult Details(int? id)
        {
            int plane = DAL.Get.Planes.ToList().Find(p => p.ID == id).ID;
            VMPlaneDetails VM = new VMPlaneDetails
            {
                Plane = DAL.Get.Planes.Include(d=> d.Days).ToList().Find(p => p.ID == id),
                Day = DAL.Get.Days.ToList().Find(d=> d.Plane.ID == id)
            };
            return View(VM);
        }

        public IActionResult DaylDetails(int? id)
        {
            if (id <1) return RedirectToAction("Index", "home");
            if(DAL.Get.User.Mail==null) return RedirectToAction("Connect", "User");
            VMDaylDetails VM = new VMDaylDetails
            {
                Day = DAL.Get.Days.Include(m => m.Meals).Include(p => p.Plane).ToList().Find(d => d.ID == id),
                Meal = DAL.Get.Meals.Include(m=> m.MealName).ToList().Find(m=> m.Day.ID == id)
            };
            return View(VM);

        }

        public IActionResult MealDetails(int? id)
        {

            Meal meal = DAL.Get.Meals.Include(f=> f.Foods).Include(m=> m.MealName).ToList().Find(m => m.ID == id);
            VMMealDetails VM = new VMMealDetails
            {
                Food = DAL.Get.FoodsInMeal.Include(f=> f.Food).ToList().Find(f => f.Meal.ID == meal.ID),
                Meal = DAL.Get.Meals.ToList().Find(m => m.ID == meal.ID),
                
                MealID = meal.ID

            };
            return View(VM);
        }
        public IActionResult ShowPlanes(int? id)
        {
            if (DAL.Get.User.Mail ==null) return RedirectToAction("Connect","User");
            List<Plane> planes1 = DAL.Get.Planes.Include(d => d.Days).ToList().FindAll(p => p.User.ID == id);
            return View(planes1);
        }
        public IActionResult AddMeal(int? id)
        {
            if (id == null) RedirectToAction(nameof(Index));
            VMAddMealOfPlane VM = new VMAddMealOfPlane
            {
                User = DAL.Get.User,
                UserID = DAL.Get.User.ID,
                TypeOfMeals = DAL.Get.TypeOfMeals.ToList(),
                Meal = new Meal(),
                Day = DAL.Get.Days.ToList().Find(D => D.ID == id),
                DayID = DAL.Get.Days.ToList().Find(D => D.ID == id).ID   
                
            };
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMeal(VMAddMealOfPlane VM)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction("Connect", "User");
            User user = DAL.Get.User;
            Day day = DAL.Get.Days.ToList().Find(d => d.ID == VM.DayID);
            TypeOfMeal type = DAL.Get.TypeOfMeals.ToList().Find(t => t.Id == VM.TypeId);
            Meal meal = new Meal
            {
                MealName = type,
                Day = day,
            };
           DAL.Get.Days.ToList().Find(d=> d.ID == day.ID).AddMeal(meal);
           DAL.Get.SaveChanges();
            
            return RedirectToAction(nameof(DaylDetails),new { id = day.ID });
        }

       

        public IActionResult AddFoodsInMeal(int? id)
        {
            if(id== null) return RedirectToAction(nameof(Index));
            Meal meal = DAL.Get.Meals.ToList().Find(m => m.ID == id);
            VMAddFoodInMeal VM = new VMAddFoodInMeal
            {
                Meal = DAL.Get.Meals.ToList().Find(m => m.ID == id),
                MealID = DAL.Get.Meals.ToList().Find(m => m.ID == id).ID,
                
                FoodInMeal = new FoodInMeal
                {
                    Meal = meal
                }
            }; 
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFoodsInMeal(VMAddFoodInMeal VMfood)
        {
            Meal meal = DAL.Get.Meals.ToList().Find(m => m.ID == VMfood.MealID);
            Food food = DAL.Get.Foods.ToList().Find(f => f.ID == VMfood.FoodID);
            DAL.Get.Meals.ToList().Find(m => m.ID == VMfood.MealID).AddFood(food, VMfood.FoodInMeal.Quantity);
            DAL.Get.SaveChanges();
            return RedirectToAction(nameof(MealDetails), new { id = meal.ID });
        }
        public IActionResult AddType()
        {
            VMAddType types = new VMAddType {
                Types = DAL.Get.TypeOfMeals.ToList(),
                Type = new TypeOfMeal()
            };
            return View(types);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddType(VMAddType VM)
        {
            DAL.Get.TypeOfMeals.Add(VM.Type);
            DAL.Get.SaveChanges();
            return RedirectToAction(nameof(ShowPlanes),new { id = DAL.Get.User.ID });
       
        }

    }
}
