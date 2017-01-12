using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MomPlusAdmin.Models
{
    public class PrenatalModel
    {
        //For the creation of prenatal record

        public int prenatalID { get; set; }
        public Guid motherID { get; set; }
        public String admin_username { get; set; }

        [DisplayName("Date Tetanus Toxoid vaccine given")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? Toxiod { get; set; }

        [DisplayName("TT Type")]
        public String TetaType { get; set; } // TT1 TT2 TT3 TT4 TT5

        [Required(ErrorMessage = "Please provide date.")]
        [DisplayName("Date given vitamin A")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? DateGivenVitA { get; set; }

        [Required(ErrorMessage = "Please provide Date.")]
        [DisplayName("Date iron was given")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? DateIronWasGiven { get; set; }

        [Required(ErrorMessage = "Please provide input")]
        [DisplayName("No. of iron was given")]
        public int NoIronWasGiven { get; set; }

        [Required(ErrorMessage = "Please provide input")]
        [DisplayName("Risk Code Detected")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? RiskCodeDetected { get; set; }

        [Required(ErrorMessage = "Please provide Date.")]
        [DisplayName("Date Terminated")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? DateTerminated { get; set; }

        [Required(ErrorMessage = "Please provide input")]
        [MaxLength(5)]
        public String OutCome { get; set; }

        [Required(ErrorMessage = "Please provide birth weight")]
        [DisplayName("Birth Weight")]
        public Double BirthWeight { get; set; }

        [Required(ErrorMessage = "Please provide input")]
        [MaxLength(10)]
        [DisplayName("Place Of Delivery:")]
        public String PlaceOfDelivery { get; set; }

        [Required(ErrorMessage = "Please provide input")]
        [MaxLength(5)]
        public String AttendedB { get; set; }

        [Required(ErrorMessage = "Please provide input")]
        [MaxLength(10)]
        public String Remarks { get; set; }

        [DisplayName("Trimester Number")]
        public String TrimesterNo { get; set; }

        [DisplayName("Trimester Date")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? Trimester { get; set; } // Trimester 1,2 ,3, dropdown


        // For editing or adding this fields to database,
        public DateTime? tt1 { get; set; }
        public DateTime? tt2 { get; set; }
        public DateTime? tt3 { get; set; }
        public DateTime? tt4 { get; set; }
        public DateTime? tt5 { get; set; }

        public DateTime? first_trimester { get; set; }
        public DateTime? second_trimester { get; set; }
        public DateTime? third_trimester { get; set; }
    
    }

    public class PostPartumModel
    {
        public int postpartumID { get; set; }
        
        public Guid motherID { get; set; }

        [Required]
        public DateTime within24hrs_after_delivery { get; set; }
        [Required]
        public DateTime within1week_after_delivery { get; set; }

        [DisplayName("Date And Time Initiated Breastfeeding")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? dateAndTime_initiated_breastfeeding { get; set; }

        [DisplayName("Date Iron Was Given")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? date_iron_was_given { get; set; }

        [DisplayName("Number Of Iron Was Given")]
        public int? no_of_iron_was_given { get; set; }

        [DisplayName("Date Of Vitamin A was given")]
        [Range(typeof(DateTime), "1/1/2016", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime? date_vitaminA_was_given { get; set; }
    }
}