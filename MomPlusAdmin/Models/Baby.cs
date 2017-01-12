using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Helpers;
namespace MomPlusAdmin.Models
{
    /// <summary>
    /// This class contains the entities for the creation of baby's account.
    /// </summary>
    public class BabyModel
    {
        public Guid baby_id { get; set; }
        public Guid mother_id { get; set; }
        /// <summary>
        /// Baby personal information
        /// </summary>
        [Required(ErrorMessage = "Please provide First Name")]
        [DisplayName("First Name:")]
        [MaxLength(15, ErrorMessage = "Please input not more than 15 characters.")]
        [MinLength(3, ErrorMessage = "Please input at least 3 characters.")]
        public String FirstName { get; set; }

       
        [Required(ErrorMessage = "Please provide Last Name")]
        [DisplayName("Last Name:")]
        [MaxLength(15, ErrorMessage = "Please input not more than 15 characters.")]
        [MinLength(3, ErrorMessage = "Please input at least 3 characters.")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Please provide Birth Date")]
        [DisplayName("Birth date:")]
        [Range(typeof(DateTime), "1/1/1916", "1/1/2116", ErrorMessage = "Provide valid date")]
        public DateTime BDate { get; set; }

        public String Gender { get; set; }

        [Required(ErrorMessage = "Please provide Weight")]
        [DisplayName("Weight:")]
        public Double Weight { get; set; }

        public int Height { get; set; }

        public DateTime DateRegister { get; set; }

        public Chart BabyDevelopment { get; set; }
    }
}