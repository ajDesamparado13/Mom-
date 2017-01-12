using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
        public class AdminMap : EntityTypeConfiguration<Admin>
        {
        
        public AdminMap() : base(){   
	                   
            this.ToTable("Admin");

            this.HasKey(ap => ap.admin_id).Property(ap => ap.admin_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); //Primary
           
            // *User personal Information
           this.Property(ap => ap.first_name).HasMaxLength(15).IsRequired();
           this.Property(ap => ap.last_name).HasMaxLength(15).IsRequired();
           this.Property(ap => ap.address).HasMaxLength(20).IsRequired();
           this. Property(ap => ap.birth_date).HasColumnType("datetime2").IsRequired();
           this.Property(ap => ap.contact_num).HasMaxLength(11).IsRequired();
           this.Property(ap => ap.gender).IsRequired();

           this.Property(ap => ap.username).HasMaxLength(10).IsRequired();
           this.Property(ap => ap.password).HasMaxLength(15).IsRequired();
           
            // *Clinic Information
           this.Property(ap => ap.Position).IsRequired();
           this.Property(ap => ap.Clinic).IsRequired();
          }
    }
}