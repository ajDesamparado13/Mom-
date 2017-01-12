using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class PostPartumMap : EntityTypeConfiguration<PostPartumEntity>
    {
        public PostPartumMap() : base() 
        {
            this.ToTable("PostPartum");

            this.HasKey(pp => pp.postpartum_id).Property(pp => pp.postpartum_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); ;

            this.Property(pp => pp.within24hrs_after_delivery).HasColumnType("datetime2");
            this.Property(pp => pp.within1week_after_delivery).HasColumnType("datetime2");

            this.Property(pp => pp.dateAndTime_initiated_breastfeeding).HasColumnType("datetime2");

            this.Property(pp => pp.date_iron_was_given).HasColumnType("datetime2");
            this.Property(pp => pp.no_of_iron_was_given);

            this.Property(pp => pp.date_vitA_was_given).HasColumnType("datetime2");
            //this.Property(p => p.admin_username).IsRequired();
            this.Property(p => p.mother_id).IsRequired();
            //this.HasRequired(pp => pp.Mother).WithMany(p => p.Postpartum).HasForeignKey(pp => pp.mother_id).WillCascadeOnDelete(true);

        }
    }
}