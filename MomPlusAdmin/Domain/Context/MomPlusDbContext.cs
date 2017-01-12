using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Map;



namespace MomPlusAdmin.Domain.Context
{
    public class MomPlusDbContext : DbContext
    {

        public MomPlusDbContext(): base("MomPlusDbContext") 
         {
         }   
        // Admin
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<MedPersonnelHistoryEntity> personnel { get; set; }
        
        //Mother
        public DbSet<Mother> Mother { get; set; }
        public DbSet<PrenatalEntity> Prenatal { get; set; }
        public DbSet<PostPartumEntity> Postpartum { get; set; }
       
        //Baby
        public DbSet<BabyEntity> Baby { get; set; }
        public DbSet<ImmunizationEntity> Immunization { get; set; }
        public DbSet<BabyDev> BabyDev { get; set; }
       
        //Inventory
        public DbSet<InventoryEntity> Inventory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Admin
            modelBuilder.Configurations.Add(new AdminMap());
            modelBuilder.Configurations.Add(new NewsFeedMap());

            modelBuilder.Configurations.Add(new MedPersonnelHistoryMap());

            //Mother
            modelBuilder.Configurations.Add(new MotherMap());
            modelBuilder.Configurations.Add(new PrenatalMap());
            modelBuilder.Configurations.Add(new PostPartumMap());
            
            //Baby
            modelBuilder.Configurations.Add(new BabyMap());
            modelBuilder.Configurations.Add(new BabyDevMap());
            modelBuilder.Configurations.Add(new MomPlusAdmin.Domain.Map.BabyMap.ImmunizationMap());
            //Inventory
            modelBuilder.Configurations.Add(new InventoryMap());
         }

    }
}