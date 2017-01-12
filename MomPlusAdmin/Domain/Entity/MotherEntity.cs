using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Entity
{
    public class Mother
    {
       
       [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid mother_id { get; set; }

        public virtual String first_name { get; set; }
        public virtual String last_name { get; set; }
        public virtual String address { get; set; }
        public virtual String contact_num { get; set; }
        public virtual DateTime birth_date { get; set; }
        public virtual String username { get; set; }
        public virtual String password { get; set; }
        
        public virtual DateTime LMP { get; set; }
        public virtual DateTime EDC { get; set; }
        public virtual DateTime date_registered { get; set; }
       
    }
       
}