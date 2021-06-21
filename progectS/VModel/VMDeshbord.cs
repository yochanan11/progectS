using progectS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.VModel
{
    public class VMDeshbord
    {
        public User User { get; set; }
        public Plane Plane { get; set; }
        public Meal Meal { get; set; }
        public MealToDAy MealToDAy { get; set; }
        public Weight Weight { get; set; }
    }
}
