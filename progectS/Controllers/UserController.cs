using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;
using progectS.VModel;

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
        public IActionResult graph()
        {
            if (DAL.Get.User.Mail == null) return RedirectToAction(nameof(Connect));
            int cuont = DAL.Get.User.Weight.Count;
            int[] Arry = new int[cuont];

            foreach (var item in DAL.Get.User.Weight)
            {
                int i = 0;
                Arry[i] = (int)item.MyWeight;
                i++;
            }
            return View(Arry);
        }
    }
}
