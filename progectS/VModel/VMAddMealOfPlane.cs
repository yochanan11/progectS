using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;

namespace progectS.VModel
{
    public class VMAddMealOfPlane
    {

        public VMAddMealOfPlane()
        {
            Plane = new Plane();
           
            TypeOfMeals = new List<TypeOfMeal>();
        }
       

        public Plane Plane { get; set; }
        public int PlaneID { get; set; }

        public User User { get; set; }

        public int UserID { get; set; }

        public Meal Meal { get; set; }

        public List<TypeOfMeal> TypeOfMeals { get; set; }

        public int TypeId { get; set; }
    }
}
