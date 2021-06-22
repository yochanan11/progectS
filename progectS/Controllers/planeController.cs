using Microsoft.AspNetCore.Mvc;
using progectS.Models;
using progectS.VModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Helpers;
using System.Web.WebPages;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace progectS.Controllers
{
    public class planeController : Controller
    {

        public IActionResult Index(int? id)
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
            List<FoodInMeal> foodIns = DAL.Get.FoodsInMeal.Include(f => f.Food).ToList();
            List<TodayFoodInMeal> foodInsT = DAL.Get.TodayFoodsInMeal.Include(f => f.Food).ToList();
            int plane = DAL.Get.Planes.ToList().Find(p => p.ID == id).ID;
            VMPlaneDetails VM = new VMPlaneDetails
            {
                Plane = DAL.Get.Planes.Include(d=> d.Days).ToList().Find(p => p.ID == id),
                Day = DAL.Get.Days. Include(d=> d.Meals).ToList().Find(d=> d.Plane.ID == id),
                Meal = DAL.Get.Meals.Include(m => m.MealName).Include(m => m.Foods).ToList().Find(m => m.Day.ID == id),
                MealToDAy = DAL.Get.MealsToDAy.Include(m => m.MealNameToDay).Include(m => m.Foods).ToList().Find(m => m.ToDay.ID == id)
            };
            var caloris = new int[7];
            var Proteins = new int[7];
            var Carbohydrates = new int[7];
            var calorisToday = new int[7];
            var ProteinsToday = new int[7];
            var CarbohydratesToday = new int[7];
            var Days = new int[7];
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
            ViewBag.Carbohydrates = Carbohydrates;
            ViewBag.Proteins = Proteins;
            ViewBag.caloris = caloris;
            ViewBag.CarbohydratesToday = CarbohydratesToday;
            ViewBag.ProteinsToday = ProteinsToday;
            ViewBag.calorisToday = calorisToday;
            ViewBag.Days = Days;
            return View(VM);
        }

        public IActionResult DaylDetails(int? id)
        {
            if (id <1) return RedirectToAction("Index", "home");
            if(DAL.Get.User.Mail==null) return RedirectToAction("Connect", "User");
            List<TodayFoodInMeal> todays = DAL.Get.TodayFoodsInMeal.Include(f => f.Food).ToList();
            List<FoodInMeal> foodIns = DAL.Get.FoodsInMeal.Include(f => f.Food).ToList();
            VMDaylDetails VM = new VMDaylDetails
            {
                Meal = DAL.Get.Meals.Include(m => m.MealName).Include(m => m.Foods).ToList().Find(m => m.Day.ID == id),
                Day = DAL.Get.Days.Include(m => m.Meals).Include(p => p.Plane).ToList().Find(d => d.ID == id),
                MealToD = DAL.Get.MealsToDAy.Include(m => m.MealNameToDay).Include(m => m.Foods).ToList().Find(m => m.ToDay.ID == id)

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

            VM.FromApi = GetFromApi();

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


        List<Food> GetFromApi()
        {

            /*
            WebClient client = new WebClient();
            string strApi = client.DownloadString("http://localhost:3000/api/food/search?query=");
            */
            HttpClient hc = new HttpClient();
            //string strApi = hc.GetStringAsync("https://api.nal.usda.gov/fdc/v1/foods/search?api_key=DEMO_KEY&dataType=Survey%20(FNDDS)&pageSize=1").Result;
            string strApi = hc.GetStringAsync("https://vast-thicket-94426.herokuapp.com/api/food/search?query=").Result;
            dynamic EntireJson = JsonConvert.DeserializeObject<dynamic>(strApi);

            dynamic Jobj = EntireJson.foods;

            ;
            FdcFood1 fdcFood = new FdcFood1();
            fdcFood.FdcFoodID = Jobj.FdcFoodID;
            fdcFood.LowerCaseDescription = Jobj.LowerCaseDescription;
            //fdcFood.MongoID = Jobj._id;

            ;

            List<Food> ready = new List<Food>();
            foreach (var item in collection)
            {
                Food f = new Food();
                f.ID = item....;
                f.Name = item...;

                ready.Add(f);
            }


            return ready;


            /*string endpoint = "https://api.cognitive.microsofttranslator.com/";

            string route = "/translate?api-version=3.0&from=he&to=en";
            string foodName = "בננה";
            string location = "eastasia";
            object[] body = new object[] { new { Text = foodName } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client1 = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "text/html");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = client1.Send(request); // await client.SendAsy*/

            }
        }
}
