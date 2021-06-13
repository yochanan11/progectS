using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class Weight
    {
        

        public Weight() { Date = DateTime.Now; }//מאתחל
        [Key]
        public int ID { get; set; }

        public User User { get; set; }//לאיזה משתמש הוא שייך

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }//תאריך  עדכון של המשקל

        public decimal MyWeight { get; set; }//משקל
    }
}

