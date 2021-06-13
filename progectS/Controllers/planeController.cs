using Microsoft.AspNetCore.Mvc;
using progectS.Models;
using progectS.VModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if(id == null) return RedirectToAction(nameof(Index), "user");
            Plane plane = DAL.Get.Planes.ToList().Find(p => p.ID == id);
            if (plane == null) return RedirectToAction(nameof(Index), "user");
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
            return RedirectToAction(nameof(Index), "user");
        }
        public IActionResult Details(int? id)
        {
            Plane plane = DAL.Get.Planes.ToList().Find(p => p.ID == id);
            return View(plane);
        }
        public IActionResult ShowPlanes(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));
            
            List<Plane> planes = new List<Plane>();
            foreach (Plane plane in DAL.Get.Planes)
            {
                if (plane.User.ID != DAL.Get.User.ID)
                {
                    return RedirectToAction("Connect", "user");
                }
                else planes.Add(plane);
            }
            return View(planes);
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
                Plane = DAL.Get.Planes.ToList().Find(p => p.ID == id),
                PlaneID = DAL.Get.Planes.ToList().Find(p => p.ID == id).ID

            };
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMeal(VMAddMealOfPlane VM)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction("Connect", "User");
            User user = DAL.Get.User;
            Plane plane = DAL.Get.Planes.ToList().Find(p => p.ID == VM.PlaneID);
            TypeOfMeal type = DAL.Get.TypeOfMeals.ToList().Find(t => t.Id == VM.TypeId);
            Meal meal = new Meal
            {
                Date = VM.Meal.Date,
                MealName = type,
                Plane = plane,
            };
            plane.AddMeal(meal);
           // DAL.Get.SaveChanges();
            
            return RedirectToAction(nameof( Details),new { id = plane.ID });
        }
    }
}
