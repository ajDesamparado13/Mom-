using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomPlusAdmin.Domain.Map
{
    public class NewsFeedMap : EntityTypeConfiguration<Calendar>
    {
        public NewsFeedMap() : base() {

            this.ToTable("Calendar");

            this.HasKey(n => n.notif_id).Property(n => n.notif_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);;

            this.Property(n => n.notif_id).IsRequired();
            this.Property(n => n.start_date).HasColumnType("datetime2").IsRequired();
            this.Property(n => n.end_date).HasColumnType("datetime2").IsRequired();
            this.Property(n => n.Event).HasColumnName("Event").HasMaxLength(500).IsRequired();

            this.Property(n => n.feedback).HasColumnName("FeedBack").IsOptional();

        }
    
    
    }
}