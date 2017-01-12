using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class MotherMap : EntityTypeConfiguration<Mother>
    {
           public MotherMap() : base(){

               this.ToTable("Mother");

               this.HasKey(m => m.mother_id).Property(m => m.mother_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

               this.Property(m => m.first_name).HasMaxLength(15).IsRequired();
               this.Property(m => m.last_name).HasMaxLength(15).IsRequired();
               this.Property(m => m.address).HasMaxLength(20).IsRequired();
               this.Property(m => m.contact_num).HasMaxLength(15).IsRequired();
               this.Property(m => m.birth_date).HasColumnType("datetime2").IsRequired();
               this.Property(m => m.username).HasMaxLength(10).IsRequired();
               this.Property(m => m.password).HasMaxLength(15).IsRequired();
               
               this.Property(m => m.EDC).IsRequired();
               this.Property(m => m.LMP).IsRequired();
               this.Property(m => m.date_registered).IsRequired();

           } 
    }
}