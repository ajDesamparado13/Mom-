using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MomPlusAdmin.Models
{
    /// <summary>
    ///* This class contains Entities for admin registration. To create and save accounts.
    /// </summary>
    public class AdminModel
    {
        #region Personal information of admin
        [Required]
        [DisplayName("First Name:")]
        [MaxLength(15, ErrorMessage = "Please input not more than 15 characters.")]
        [MinLength(3, ErrorMessage = "Please input at least 3 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only.")]
        public String Fname { get; set; }
        
        [Required]
        [DisplayName("Last Name: ")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only.")]
        [MaxLength(15, ErrorMessage = "Please input not more than 15 characters.")]
        [MinLength(3, ErrorMessage="Please input at least 3 characters.")]
        public String LName { get; set; }

        [Required]
        [DisplayName("Address: ")]
        [MaxLength(20, ErrorMessage = "Please input at least 20 characters.")]
        [MinLength(2, ErrorMessage = "Please input at least 2 characters.")]
        public String Address { get; set; }

        [Required]
        [DisplayName("Date of Birth: ")]
        public DateTime Bday { get; set; }

        [Required]
        [DisplayName("Contact Number: ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Number must be numeric")]
        [MaxLength(11, ErrorMessage = "Please input valid Contact Number.")]
        [MinLength(9, ErrorMessage = "Please input valid Contact Number")]
        public String ContactNum { get; set; }

        [Required]
        public String Gender { get; set; }

        #endregion

        #region For account or access
        public int ID { get; set; }
        [Required]
        [DisplayName("User Name: ")]
        [MaxLength(10, ErrorMessage = "Please input valid Username. It must not more than 10 characters.")]
        [MinLength(5, ErrorMessage = "Please input at least 5 characters.")]
        public String Username { get; set; }


        [Required]
        [DisplayName("Password: ")]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Please input password not more than 15 characters.")]
        [MinLength(5, ErrorMessage = "Please input at least 5 characters.")]
        public String Password { get; set; }


        [Required]
        [DisplayName("Position: ")]
        public String Position { get; set; }

        [Required]
        [DisplayName("Clinic: ")]
        public String Clinic { get; set; }


        #endregion
    }
        #region Calendar model
    public class CalendarModel {

        public int id { get; set; }
        [Required]
        public String Event { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public String Feedback { get; set; }

    }
    #endregion

        #region Login Model

    public class LoginModel
    {
        [Required(ErrorMessage = "Please provide username")]
        [DisplayName("Username")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Please provide password")]
        [DisplayName("Password: ")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

    
    }
#endregion
}