using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class DAL : DbContext
    {
        private static DAL Data;


        public User User = new User { FirstName = "התחבר" };

        private DAL() : base(@"data source=DESKTOP-L24RP45\SQLEXPRESS;initial catalog = progects; user id = sa; password=1234;")
        {
            Database.SetInitializer<DAL>(new DropCreateDatabaseIfModelChanges<DAL>());
            if (Foods.Count() == 0) Seed();
        }
        public static DAL Get { get { if (Data == null) Data = new DAL(); return Data; } }

        private void Seed()
        {
            Food food = new Food//בונה מאכל שיהיה בטוח 
            {
                Name = "מים",
                Caloris = 0
            };
            TypeOfMeal typeOfMeal = new TypeOfMeal//בונה ארוחה שתהיה בטוח
            {
                Name = "ארוחת בוקר"
            };
            Foods.Add(food);//מוסיף את המאכל לדאטא
            TypeOfMeals.Add(typeOfMeal);
            SaveChanges();//שמירה והחלפה
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FoodInMeal> FoodsInMeal { get; set; }
        public DbSet<Weight> Indices { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<TypeOfMeal> TypeOfMeals { get; set; }
        public DbSet<Day> Days { get; set; }


    }
}
