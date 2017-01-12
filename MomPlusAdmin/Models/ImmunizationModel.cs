using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MomPlusAdmin.Models
{
    public class ImmunizationModel
    {

        // Immunization
        public int? immunization_id { get; set; }
        public Guid baby_id { get; set; }

        [Required]
        public DateTime ImmunizationDate { get; set; }

        //Immunizationmeds
        public DateTime? six_to_twelvemos { get; set; }
        public DateTime? twelve_to_59mos { get; set; }
        public DateTime? twelve59mos_dosage1 { get; set; }
        public DateTime? twelve59mos_dosage2 { get; set; }
    }
}