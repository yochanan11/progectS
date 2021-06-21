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
            var Weights = new List<decimal>();
            var Days = new List<int>();

            foreach (Weight item in DAL.Get.User.Weight)
            {
                Weights.Add(item.MyWeight);
                Days.Add(item.Date.Day);
            }
            ViewBag.Weight = Weights.ToList();
            ViewBag.Days = Days.ToList();

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
        public IActionResult Deshbord(int? id)
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction(nameof(Connect));
            VMDeshbord VM = new VMDeshbord
            {
                User= DAL.Get.User,
                Plane = DAL.Get.Planes.Include(d=> d.Days).ToList().Find(p=> p.ID == id),
                Meal = DAL.Get.Meals.Include(f=> f.Foods).ToList().Find(m=> m.Day.Plane.ID == id),
                MealToDAy = DAL.Get.MealsToDAy.Include(f => f.Foods).ToList().Find(m => m.ToDay.Plane.ID == id),
                Weight = DAL.Get.Indices.ToList().Find(w=> w.ID== id)


            };
            var caloris = new int[7];
            var Proteins = new int[7];
            var Carbohydrates = new int[7];
            var calorisToday = new int[7];
            var ProteinsToday = new int[7];
            var CarbohydratesToday = new int[7];
            var Days = new int[7];
            var Weights = new List<decimal>();
            var DaysL = new List<int>();
            Plane plane1 = DAL.Get.Planes.ToList().Find(p => p.ID == id);

            for (int i = 0; i < plane1.Days.Count; i++)
            {
                caloris[i] = plane1.Days[i].SumALLProperties.SumCaloris;
                Carbohydrates[i] = plane1.Days[i].SumALLPropertiesToDay.SumCarbohydrates;
                Proteins[i] = plane1.Days[i].SumALLProperties.SumProteins;
                calorisToday[i] = plane1.Days[i].SumALLPropertiesToDay.SumCaloris;
                ProteinsToday[i] = plane1.Days[i].SumALLPropertiesToDay.SumProteins;
                Carbohydrates[i] = plane1.Days[i].SumALLProperties.SumCarbohydrates;
            }
            int x = 0;
            foreach (Day item in plane1.Days)
            {
                Days[x] = item.Date.Day;
                x++;
            }
            foreach (Weight item in DAL.Get.User.Weight)
            {
                Weights.Add(item.MyWeight);
                DaysL.Add(item.Date.Day);
            }
            ViewBag.Carbohydrates = Carbohydrates;
            ViewBag.Proteins = Proteins;
            ViewBag.caloris = caloris;
            ViewBag.CarbohydratesToday = CarbohydratesToday;
            ViewBag.ProteinsToday = ProteinsToday;
            ViewBag.calorisToday = calorisToday;
            ViewBag.Days = Days;
            ViewBag.Weight = Weights.ToList();
            ViewBag.DaysL = DaysL.ToList();
            return View(VM); 

        }

    }
}

