using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class BabyDevMap : EntityTypeConfiguration<BabyDev>
    {
       public BabyDevMap() : base() 
        {
            this.ToTable("BabyDev");

            this.HasKey(ap => ap.id).Property(ap => ap.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); //Primary

            this.Property(a => a.baby_id).IsRequired();

            this.Property(a => a.first_month);
            this.Property(a => a.second_month);
            this.Property(a => a.third_month);
            this.Property(a => a.fourth_month);
            this.Property(a => a.fift_month);
            this.Property(a => a.six_month);
            this.Property(a => a.seven_month);
            this.Property(a => a.eight_month);
            this.Property(a => a.nine_month);
            this.Property(a => a.ten_month);
            this.Property(a => a.eleven_month);
            this.Property(a => a.twelve_month);
        }
    }
}