using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    // מאכל מ-api
    public class FdcFood1
    {
        public FdcFood1() { Nutrients = new List<FoodNutrients>(); }

        public string MongoID { get; set; }

        [Key]
        public int FdcFoodID { get; set; }                   // מזהה מאכל

        [Display(Name = "שם מאכל באותיות גדולות")]
        public string Description { get; set; }              // שם מאכל באותיות גדולות

        [Display(Name = "שם מאכל")]
        public string LowerCaseDescription { get; set; }     // שם מאכל באותיות קטנות

        [Display(Name = "סוג מאכל")]
        public string DataType { get; set; }                // סוג מאכל

        [Display(Name = "gtin Upc")]
        public string GtinUpc { get; set; }                 // gtinUpc

        [Display(Name = "תאריך פרסום")]
        public string PublishedDate { get; set; }           // תאריך פרסום

        [Display(Name = "בעל מותג")]
        public string BrandOwner { get; set; }              // בעל מותג

        [Display(Name = "שם מותג")]
        public string brandName { get; set; }               // שם מותג

        [Display(Name = "רכיבים")]
        public string Ingredients { get; set; }             // רכיבים

        [Display(Name = "איזור חנות")]
        public string MarketCountry { get; set; }           // איזור חנות

        [Display(Name = "קטגוריית מאכל")]
        public string FoodCategory { get; set; }            // קטגוריית מאכל

        [Display(Name = "All Highlight Fields")]
        public string AllHighlightFields { get; set; }      // All Highlight Fields

        [Display(Name = "score")]
        public int Score { get; set; }                      // score

        // רכיבים במאכל
        public List<FoodNutrients> Nutrients { get; set; }
    }
}
