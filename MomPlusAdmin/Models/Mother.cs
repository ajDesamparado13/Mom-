using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MomPlusAdmin.Models
{
    /// <summary>
    /// This class contains the Entities for the creation of Mother account.
    /// </summary>
    public class MotherModel
    {
        #region Mother Personal Information

        public Guid id { get; set; }

        public String admin_username { get; set; }

        [Required(ErrorMessage="Please provide First Name")]
        [DisplayName("First Name:")]
        [MaxLength(15, ErrorMessage = "Please input not more than 15 characters.")]
        [MinLength(3, ErrorMessage = "Please input at least 3 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only.")]
        public String MotherFName { get; set; }

        [Required(ErrorMessage="Please provide Last Name")]
        [DisplayName("Last Name:")]
        [MaxLength(15, ErrorMessage = "Please input not more than 15 characters.")]
        [MinLength(3, ErrorMessage = "Please input at least 3 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only.")]
        public String MotherLname { get; set; }

        [Required(ErrorMessage="Please Provide Address")]
        [DisplayName("Address:")]
        [MaxLength(15, ErrorMessage = "Please input not more than 15 characters.")]
        [MinLength(2, ErrorMessage = "Please input at least 2 characters.")]
        public String MotherAddress { get; set; }

        [Required(ErrorMessage="Please provide Contact Number")]
        [DisplayName("Contact Number:")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Number must be numeric")]
        [MaxLength(11, ErrorMessage = "Please input valid Contact Number.")]
        [MinLength(9, ErrorMessage = "Please input valid Contact Number")]
        public String MotherCnum { get; set; }
        
        [Required(ErrorMessage="Please provide Birthdate")]
        [DisplayName("Birth Date:")]
        public DateTime MotherBdate { get; set; }

        public int MotherAge { get; set; }
        #endregion
        
        #region Account
      

        [Required(ErrorMessage="Please provide Username")]
        [DisplayName("User Name: ")]
        [MaxLength(10, ErrorMessage = "Please input valid Username. It must not more than 10 characters.")]
        [MinLength(5, ErrorMessage = "Please input at least 5 characters.")]
        public String motherUsername { get; set; }

        [Required(ErrorMessage="Please provide Password")]
        [DisplayName("Password: ")]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Please input password not more than 15 characters.")]
        [MinLength(5, ErrorMessage = "Please input at least 5 characters.")]
        public String motherPassword { get; set; }
        #endregion

        #region Check Ups
        [Required(ErrorMessage= "Please provide Last Menstrual Period ")]
        [DisplayName("Last Menstrual Period")]
        public DateTime LMP { get; set; }

        [DisplayName("Estimated Date Conceived")]
        public DateTime EDC { get; set; }

        [DisplayName("Date Registered")]
        public DateTime DateRegistered { get; set; }

        public DateTime FirstPostpartum { get; set; }
        public DateTime SecondPostpartum { get; set; }
        #endregion

       
    }
}