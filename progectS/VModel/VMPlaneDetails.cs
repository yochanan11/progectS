using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;

namespace progectS.VModel
{
    public class VMPlaneDetails
    {
        public Plane Plane { get; set; }

        public Day Day { get; set; }

        public Meal Meal { get; set; }

        public MealToDAy MealToDAy { get; set; }
    }
}
