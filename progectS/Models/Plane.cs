using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    public class Plane
    {
        public Plane()
        {
            Days = new List<Day>();
            Date = DateTime.Now; 
        }
        [Key]
        public int ID { get; set; }

        [Display(Name = "שם התוכנית")]
        public string Name { get; set; }

        public User User { get; set; }


        public List<Day> Days { get; set; }

        [Display(Name = "תאריך תוכנית")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public SumALLProperties SumALLProperties
        {
            get
            {
                SumALLProperties Sum = new SumALLProperties(0, 0, 0);
                foreach (Day Day in Days)
                {
                    Sum.SumCaloris += Day.SumALLProperties.SumCaloris;
                    Sum.SumCarbohydrates += Day.SumALLProperties.SumCarbohydrates;
                    Sum.SumProteins += Day.SumALLProperties.SumProteins;
                }
                return Sum;
            }
        }

        public SumALLProperties SumALLPropertiesToDay
        {
            get
            {
                SumALLProperties Sum = new SumALLProperties(0, 0, 0);
                foreach (Day Day in Days)
                {
                    Sum.SumCaloris += Day.SumALLPropertiesToDay.SumCaloris;
                    Sum.SumCarbohydrates += Day.SumALLProperties.SumCarbohydrates;
                    Sum.SumProteins += Day.SumALLProperties.SumProteins;
                }
                return Sum;
            }
        }

        public void AddDays()
        {
            string[] Darry = new string[7];
            switch (Date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    Darry[0] = "יום ראשון"; Darry[1] = "יום שני"; Darry[2] = "יום שלישי"; Darry[3] = "יום רביעי"; Darry[4] = "יום חמישי"; Darry[5] = "יום שישי"; Darry[6] = "שבת קודש";
                    break;
                case DayOfWeek.Monday:
                    Darry[6] = "יום ראשון"; Darry[0] = "יום שני"; Darry[1] = "יום שלישי"; Darry[2] = "יום רביעי"; Darry[3] = "יום חמישי"; Darry[4] = "יום שישי"; Darry[5] = "שבת קודש";
                    break;
                case DayOfWeek.Tuesday:
                    Darry[5] = "יום ראשון"; Darry[6] = "יום שני"; Darry[0] = "יום שלישי"; Darry[1] = "יום רביעי"; Darry[2] = "יום חמישי"; Darry[3] = "יום שישי"; Darry[4] = "שבת קודש";
                    break;
                case DayOfWeek.Wednesday:
                    Darry[4] = "יום ראשון"; Darry[5] = "יום שני"; Darry[6] = "יום שלישי"; Darry[0] = "יום רביעי"; Darry[1] = "יום חמישי"; Darry[2] = "יום שישי"; Darry[3] = "שבת קודש";
                    break;
                case DayOfWeek.Thursday:
                    Darry[3] = "יום ראשון"; Darry[4] = "יום שני"; Darry[5] = "יום שלישי"; Darry[6] = "יום רביעי"; Darry[0] = "יום חמישי"; Darry[1] = "יום שישי"; Darry[2] = "שבת קודש";
                    break;
                case DayOfWeek.Friday:
                    Darry[2] = "יום ראשון"; Darry[3] = "יום שני"; Darry[4] = "יום שלישי"; Darry[5] = "יום רביעי"; Darry[6] = "יום חמישי"; Darry[0] = "יום שישי"; Darry[1] = "שבת קודש";
                    break;
                case DayOfWeek.Saturday:
                    Darry[1] = "יום ראשון"; Darry[2] = "יום שני"; Darry[3] = "יום שלישי"; Darry[4] = "יום רביעי"; Darry[5] = "יום חמישי"; Darry[6] = "יום שישי"; Darry[0] = "שבת קודש";
                    break;
                default:
                    Darry[0] = "יום ראשון"; Darry[1] = "יום שני"; Darry[2] = "יום שלישי"; Darry[3] = "יום רביעי"; Darry[4] = "יום חמישי"; Darry[5] = "יום שישי"; Darry[6] = "שבת קודש";
                    break;
            }
            
            Days = new List<Day>();
            for (int i = 0; i < Darry.Length; i++)
            {
                Day day = new Day { DayName = Darry[i], Date = Date.AddDays(i), Plane = this };
                Days.Add(day);
            }

        }
    }

    
}
