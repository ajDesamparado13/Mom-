using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace MomPlusAdmin.Domain.Entity
{
    public class Admin
    {
              
        /// <summary>
        /// Primary key
        /// </summary>

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int admin_id { get; set; }
        
        /// <summary>
        /// Personal Info
        /// </summary>
        public virtual String first_name { get; set; }
        public virtual String last_name { get; set; }
        public virtual String address { get; set; }
        
        public virtual DateTime birth_date { get; set; }
        public virtual String contact_num { get; set; }
        public virtual String gender { get; set; }

        /// <summary>
        /// For login and profile
        /// </summary>
        public virtual String username { get; set; }
        public virtual String password { get; set; }
        
        /// <summary>
        /// Clinic Information
        /// </summary>
        public virtual String Position { get; set; }
        public virtual String Clinic { get; set; }

    }

    public class Calendar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? notif_id { get; set; }

        public virtual DateTime start_date { get; set; }
        public virtual DateTime end_date { get; set; }
        public virtual String Event { get; set; }
        
        public virtual int feedback { get; set; }

        
    }
}