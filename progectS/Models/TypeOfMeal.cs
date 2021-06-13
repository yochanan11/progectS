using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class TypeOfMeal//סוגי הארוחות
    {
        public TypeOfMeal() { }
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם הארוחה")]
        public string Name { get; set; }//שם הארוחה

    }
}
