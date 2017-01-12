using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Entity
{
    public class MedPersonnelHistoryEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int id { get; set; }
        public virtual Guid user_id { get; set; }

        public virtual String admin_who_registered { get; set; }
        //Prenatal Properties
        public virtual String p_gave_tt1 { get; set; }
        public virtual String p_gave_tt2 { get; set; }
        public virtual String p_gave_tt3 { get; set; }
        public virtual String p_gave_tt4 { get; set; }
        public virtual String p_gave_tt5 { get; set; }
        public virtual String p_gave_vit_a { get; set; }
        public virtual String p_gave_iron { get; set; }

        //Postpartum properties
        public virtual String pp_gave_vit_a { get; set; }
        public virtual String pp_gave_iron { get; set; }

        //Immunization properties
        public virtual String i_6_to_12mos { get; set; }
        public virtual String i_12_to_59mos { get; set; }
        public virtual String i_12_to_59mos_dosage1 { get; set; }
        public virtual String i_12_to_59mos_dosage2 { get; set; }


    }
}