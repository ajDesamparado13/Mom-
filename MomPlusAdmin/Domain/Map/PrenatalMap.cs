using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class PrenatalMap : EntityTypeConfiguration<PrenatalEntity>
    {
        public PrenatalMap(): base()
        {
            this.ToTable("Prenatal");

            this.HasKey(p => p.prenatal_id).Property(p => p.prenatal_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.tt1).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.tt2).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.tt3).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.tt4).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.tt5).HasColumnType("datetime2").IsOptional();

            this.Property(p => p.date_given_vit_A).HasColumnType("datetime2").IsRequired();

            this.Property(p => p.date_iron_was_given).HasColumnType("datetime2").IsRequired();
            this.Property(p => p.no_iron_was_given).IsRequired();

            this.Property(p => p.risk_code_detected).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.date_terminated).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.outcome).IsOptional();
            this.Property(p => p.birth_weight).IsRequired();
            this.Property(p => p.place_Of_delivery).IsRequired();
            this.Property(p => p.attended_B).IsRequired();

            this.Property(p => p.remarks).IsOptional();

            this.Property(p => p.first_trimester).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.second_trimester).HasColumnType("datetime2").IsOptional();
            this.Property(p => p.third_trimester).HasColumnType("datetime2").IsOptional();

            this.Property(p => p.ultrasound_image).HasColumnType("varbinary").IsOptional();
            //this.Property(p => p.admin_username).IsRequired();
            this.Property(n => n.mother_id).IsRequired();
            //this.HasRequired(n => n.Mother).WithMany(a => a.Prenatal).HasForeignKey(n => n.mother_id).WillCascadeOnDelete(true);
            
        }
    }
}