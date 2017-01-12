using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace MomPlusAdmin.Domain.Entity
{
    public class PostPartumEntity
    {
        public virtual int postpartum_id { get; set; }

        public virtual Guid mother_id { get; set; }
       
        /// <summary>
        /// Date postpartum visit
        /// </summary>
        public virtual DateTime within24hrs_after_delivery { get; set; }
        public virtual DateTime within1week_after_delivery { get; set; }

        public virtual DateTime? dateAndTime_initiated_breastfeeding { get; set; }

        //Micronutrient 
        public virtual DateTime? date_iron_was_given { get; set; }
        public virtual int? no_of_iron_was_given { get; set; }

        public virtual DateTime? date_vitA_was_given { get; set; }

        //public virtual String admin_username { get; set; }
    }

    public class PrenatalEntity {


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int prenatal_id { get; set; }

        public virtual Guid mother_id { get; set; }
        //public virtual String admin_username { get; set; }

        public virtual DateTime? tt1 { get; set; }
        public virtual DateTime? tt2 { get; set; }
        public virtual DateTime? tt3 { get; set; }
        public virtual DateTime? tt4 { get; set; }
        public virtual DateTime? tt5 { get; set; }

        public virtual DateTime? date_given_vit_A { get; set; }
        
        public virtual DateTime? date_iron_was_given { get; set; }
        public virtual int no_iron_was_given { get; set; }
        
        public virtual DateTime? risk_code_detected { get; set; }
        
        public virtual DateTime? date_terminated { get; set; }
        
        public virtual String outcome { get; set; }
        public virtual Decimal birth_weight { get; set; }
        public virtual String place_Of_delivery { get; set; }
        public virtual String attended_B { get; set; }
   
        public virtual String remarks { get; set; }

        //Prenatal Visits
        public virtual DateTime? first_trimester { get; set; }
        public virtual DateTime? second_trimester { get; set; }
        public virtual DateTime? third_trimester { get; set; }

        public virtual Byte[] ultrasound_image { get; set; }
    }

}