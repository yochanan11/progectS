using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;
using progectS.VModel;
using System.Data.Entity;

namespace progectS.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            List <Plane> plane = DAL.Get.Planes.ToList();
            return View(plane);
        }
        public IActionResult Connect()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Connect(User user)
        {
            User user1 = DAL.Get.Users.ToList().Find(u => u.Mail == user.Mail && u.Password == user.Password);
            if (user1 == null) return RedirectToAction(nameof(NoConnect));
            DAL.Get.User = user1;
            return RedirectToAction("ShowPlanes", "plane",new {id=DAL.Get.User.ID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registery(User user)
        {
            DAL.Get.Users.Add(user);
            DAL.Get.User = user;
            DAL.Get.SaveChanges();
            return RedirectToAction("ShowPlanes", "plane",new { id = DAL.Get.User.ID });
        }

        public IActionResult NoConnect()
        {
            User user = new User();
            return View(user);
        }

        public IActionResult UnConnect(int? id)
        {
            DAL.Get.User = new User { FirstName = "התחבר" };
            return RedirectToAction(nameof(Index), "home");
        }

        public IActionResult SohuUser(int? id)
        {
            VMAddIndices user = new VMAddIndices
            {
                User  = DAL.Get.User,
                Weight = DAL.Get.Indices.ToList().Find(w=> w.User.ID == DAL.Get.User.ID)
            };

            return View(user);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIndices(VMAddIndices vmi)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction(nameof(Connect));
            User user = DAL.Get.User;
            DAL.Get.Users.ToList().Find(u=>u.ID == user.ID).AddWeight(vmi.Weight);
            DAL.Get.SaveChanges();
            return RedirectToAction(nameof(SohuUser), new { id = user.ID });
        }
        public IActionResult AddPlane()
        {
            Plane plane = new Plane();
            return View(plane);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPlane(Plane plane)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction(nameof(Connect));
            User user = DAL.Get.User;
            user.AddPlan(plane);
            DAL.Get.SaveChanges();
            return RedirectToAction("ShowPlanes", "plane", new { id = user.ID });
        }
        public IActionResult ToDay()
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction(nameof(Connect));
            List<Plane> planes = DAL.Get.Planes.ToList().FindAll(p => p.User.ID == DAL.Get.User.ID);
            Day day1 = new Day();
            foreach (Plane plane1 in planes)
            {
                foreach (Day day in plane1.Days)
                {
                    if (day.Date == DateTime.Now) day1 = day;
                }
            }
            if (day1 == null) return RedirectToAction(nameof(Connect));
            VMToDay VM = new VMToDay
            {
                User = DAL.Get.User,
                Day = day1,
                ToDAy = new ToDAy(),
                MealToDAy = new MealToDAy(),
                type = DAL.Get.TypeOfMeals.ToList(),
                TodayFoodsInMeal = new TodayFoodInMeal()
            };
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToDay(VMToDay VM)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction(nameof(Connect));
            User User = DAL.Get.User;
            Food food = DAL.Get.Foods.ToList().Find(f => f.ID == VM.FoodId);
            TypeOfMeal type = DAL.Get.TypeOfMeals.ToList().Find(t => t.Id == VM.TypeId);
            User.AddFoodToDAy(VM.Date, food, type, VM.TodayFoodsInMeal.Quantity, VM.ToDAy.Mood);
            DAL.Get.SaveChanges();
            return RedirectToAction(nameof(ShowToday));
        }
        public IActionResult ShowToday()
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction(nameof(Connect));
            List<Plane> planes = DAL.Get.Planes.ToList().FindAll(p => p.User.ID == DAL.Get.User.ID);
            Day day1 = new Day();
            foreach (Plane plane1 in planes)
            {
                foreach (Day day in plane1.Days)
                {
                    if (day.Date.Date == DateTime.Now.Date) day1 = day;
                }
            }
            if (day1 == null) return RedirectToAction(nameof(Connect));

            VMToDay VM = new VMToDay
            {
                Day = day1,
                User = DAL.Get.User,
                ToDAy = DAL.Get.ToDAys.ToList().Find(t => t.User.ID == DAL.Get.User.ID),
                MealToDAy = DAL.Get.MealsToDAy.ToList().Find(m => m.ToDAy.User.ID == DAL.Get.User.ID),
                TodayFoodsInMeal = DAL.Get.TodayFoodsInMeal.ToList().Find(t => t.MealToDAy.ToDAy.User.ID == DAL.Get.User.ID),
                type = DAL.Get.TypeOfMeals.ToList()
            };
            return View(VM);
            
        }
        
    }
}
