using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class User
    {
        public User()
        {
          
            Plans = new List<Plane>();
            Weight = new List<Weight>();
            BirthOfDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "שם פרטי")]
        [Required(ErrorMessage = "שדה חובה")]
        public string FirstName { get; set; }

        [Display(Name = "שם משפחה")]
        [Required(ErrorMessage = "שדה חובה")]
        public string LastName { get; set; }

        [Display(Name = "שם מלא")]
        public string FuulName { get { return FirstName + " " + LastName; } }

        [Display(Name = "תאריך לידה")]
        [Required(ErrorMessage = "שדה חובה")]
        [DataType(DataType.Date)]
        public DateTime BirthOfDate { get; set; }//תאריך לידה

        [Display(Name = "אימייל")]
        [Required(ErrorMessage = "שדה חובה")]
        [EmailAddress(ErrorMessage = "נא הכנס כתובת מייל נכונה")]

        public string Mail { get; set; }

        [Display(Name = "סיסמא")]
        [Required(ErrorMessage = "שדה חובה")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Plane> Plans { get; set; }//,תוכניות

        public List<Weight> Weight { get; set; }//,משקל

      

        [Display(Name = "גיל")]
        public int Age//נותן את הגיל
        {
            get
            {
                return DateTime.Now.Year - BirthOfDate.Year;
            }
        }

        public void AddPlan(Plane plans)//מוסיף תוכנית ליוזר
        {
            Plans.Add(plans);
            plans.AddDays();
            plans.User = this;
        }
        public void AddWeight(Weight weight)//להוסיף משקל
        {
            Weight.Add(weight);
            weight.User = this;
            /*Weight.Add(new Weight { MyWeight = weight, User = this });*/
        }

    }
}
/*public void AddFoodToDAy(DateTime day, Food food, TypeOfMeal type, double quantity, string mood)
{
    ToDAy ToDAy;
    ToDAy = ToDAys.Find(t => t.Date == day);
    if (ToDAy == null)
    {
        ToDAy = new ToDAy { Date = day, User = this };
        ToDAys.Add(ToDAy);
    }
    ToDAy.AddFood(food, type, quantity, mood);
}*/