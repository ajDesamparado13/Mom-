using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class InventoryMap : EntityTypeConfiguration<InventoryEntity>
    {
        public InventoryMap(): base()
        {
            this.ToTable("Inventory");

            this.HasKey(p => p.item_id).Property(p => p.item_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(i => i.unit_of_issued).IsOptional();
            this.Property(i => i.item_description).IsOptional();
            this.Property(i => i.qty).IsOptional();
            this.Property(i => i.unit_cost).IsOptional();
            this.Property(i => i.total_amount).IsOptional();


        
        }
    }
}