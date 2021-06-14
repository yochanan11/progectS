using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using progectS.Models;

namespace progectS.VModel
{
    public class VMAddType
    {
        public VMAddType()
        {
            Types = new List<TypeOfMeal>();
        }
        public TypeOfMeal Type { get; set; }

        public List<TypeOfMeal> Types { get; set; }
    }
}
