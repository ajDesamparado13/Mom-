using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class BabyMap : EntityTypeConfiguration<BabyEntity>
    {
        public BabyMap() : base()
        {
            this.ToTable("Baby");

            this.HasKey(b => b.baby_id).Property(b => b.baby_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.Property(b => b.first_name).IsRequired();
            this.Property(b => b.last_name).IsRequired();
            this.Property(b => b.gender).IsRequired();
            this.Property(b => b.weight).IsRequired();
            this.Property(b => b.height).IsRequired();
            this.Property(b => b.date_registered).HasColumnType("datetime2").IsRequired();
            this.Property(b => b.birth_date).HasColumnType("datetime2").IsRequired();
            
            this.Property(n => n.mother_id).IsRequired();
            //this.HasRequired(n => n.mother).WithMany(a => a.babies).HasForeignKey(n => n.mother_id).WillCascadeOnDelete(true);
        }

        public class ImmunizationMap : EntityTypeConfiguration<ImmunizationEntity>
        {
            public ImmunizationMap() : base()
            {
                this.ToTable("Immunization");

                this.HasKey(i => i.immunization_id).Property(i => i.immunization_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                this.Property(i => i.immunization_date).HasColumnType("datetime2").IsRequired();
                this.Property(i => i.six_to_twelvemos).HasColumnType("datetime2");
                this.Property(i => i.twelve_to_59mos).HasColumnType("datetime2");
                this.Property(i => i.twelve59mos_dosage1).HasColumnType("datetime2");
                this.Property(i => i.twelve59mos_dosage2).HasColumnType("datetime2");
                //this.Property(p => p.admin_username).IsRequired();
                this.Property(n => n.baby_id).IsRequired();
                //this.HasRequired(n => n.baby).WithMany(a => a.immunizations).HasForeignKey(n => n.baby_id).WillCascadeOnDelete(true);
            
            }
        }
    }
}