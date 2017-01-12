using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace MomPlusAdmin.Domain.Entity
{
    public class BabyEntity
    {
       
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid baby_id { get; set; }

        public virtual Guid mother_id { get; set; }
        
       
        public virtual String first_name { get; set; }
        public virtual String last_name { get; set; }
        public virtual String gender { get; set; }
        public virtual Decimal weight { get; set; }
        public virtual int height { get; set; }
        public virtual DateTime birth_date { get; set; }
        public virtual DateTime date_registered { get; set; }

        
    }

    public class ImmunizationEntity
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int immunization_id { get; set; }

        public virtual Guid baby_id { get; set; }
        
        //public virtual String admin_username { get; set; }

        public DateTime immunization_date { get; set; }

        public virtual DateTime? six_to_twelvemos { get; set; }
        public virtual DateTime? twelve_to_59mos { get; set; }
        public virtual DateTime? twelve59mos_dosage1 { get; set; }
        public virtual DateTime? twelve59mos_dosage2 { get; set; }
       
    }


}