using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class Food
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "שם מאכל")]
        public string Name { get; set; }
        
        [Display(Name = "מספר הקלוריות")]
        public int Caloris { get; set; }

        [Display(Name = "תמונה")]
        public byte[] Photo { get; set; }
        //ממיר תמונה לבייטים
        public IFormFile SetPhoto { set { if (value != null) Photo = new ParsePhoto().Get(value); } }


    }
}
