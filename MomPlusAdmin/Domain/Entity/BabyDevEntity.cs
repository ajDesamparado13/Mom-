using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Entity
{
    public class BabyDev
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public Guid baby_id { get; set; }

        public Decimal first_month { get; set; }
        public Decimal second_month { get; set; }
        public Decimal third_month { get; set; }
        public Decimal fourth_month { get; set; }
        public Decimal fift_month { get; set; }
        public Decimal six_month { get; set; }
        public Decimal seven_month { get; set; }
        public Decimal eight_month { get; set; }
        public Decimal nine_month { get; set; }
        public Decimal ten_month { get; set; }
        public Decimal eleven_month { get; set; }
        public Decimal twelve_month { get; set; }
     }
}