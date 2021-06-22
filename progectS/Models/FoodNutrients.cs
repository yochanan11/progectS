using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    // רכיבי מאכל מ-api
    public class FoodNutrients
    {
        public FdcFood1 FdcFood1 { get; set; }            // שיוך למאכל

        [Key]
        [Display(Name = "מזהה מאכל")]
        public int NutrientID { get; set; }             // ID
        [Display(Name = "שם רכיב")]
        public string Name { get; set; }                // שם רכיב
        [Display(Name = "מספר רכיב")]
        public int NutrientNumber { get; set; }         // מספר רכיב
        [Display(Name = "יחידה")]
        public string Unit { get; set; }                   // יחידה
        [Display(Name = "derivation Code")]
        public int DerivationCode { get; set; }         // derivation Code
        [Display(Name = "Derivation Description")]
        public int DerivationDescription { get; set; }  // Derivation Description
        [Display(Name = "ערך")]
        public int Value { get; set; }                  // ערך


    }
}
