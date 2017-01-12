using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Entity
{
    public class InventoryEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int item_id { get; set; }

        public virtual String unit_of_issued { get; set; }
        public virtual String item_description { get; set; }
        public virtual int qty { get; set; }
        public virtual Decimal unit_cost { get; set; }
        public virtual Decimal total_amount { get; set; }
    }
}