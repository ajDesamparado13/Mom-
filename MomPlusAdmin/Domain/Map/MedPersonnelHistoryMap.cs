using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class MedPersonnelHistoryMap : EntityTypeConfiguration<MedPersonnelHistoryEntity>
    {
       public MedPersonnelHistoryMap(): base()
        {
            this.ToTable("PersonnelMedHistory");

            this.HasKey(p => p.id).Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.user_id).IsRequired();
            this.Property(p => p.admin_who_registered).IsRequired();

            this.Property(p => p.p_gave_tt1).IsOptional();
            this.Property(p => p.p_gave_tt2).IsOptional();
            this.Property(p => p.p_gave_tt3).IsOptional();
            this.Property(p => p.p_gave_tt4).IsOptional();
            this.Property(p => p.p_gave_tt5).IsOptional();
            this.Property(p => p.p_gave_vit_a).IsOptional();
            this.Property(p => p.p_gave_iron).IsOptional();

            this.Property(pp => pp.pp_gave_iron).IsOptional();
            this.Property(pp => pp.pp_gave_vit_a).IsOptional();

            this.Property(i => i.i_6_to_12mos).IsOptional();
            this.Property(i => i.i_12_to_59mos).IsOptional();
            this.Property(i => i.i_12_to_59mos_dosage1).IsOptional();
            this.Property(i => i.i_12_to_59mos_dosage2).IsOptional();

        }
    }
}