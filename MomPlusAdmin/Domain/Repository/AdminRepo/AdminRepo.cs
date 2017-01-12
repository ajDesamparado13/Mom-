using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using System.Data;

namespace MomPlusAdmin.Domain.Repository.AdminRepo
{
    public class AdminRepo
    {
         MomPlusDbContext context = new MomPlusDbContext();


         public AdminRepo() { }

        public IEnumerable<Admin> getAdmin()
        {
            return context.Admins.ToList();
        }

        public Admin getAdminByID(int id)
        {
            return context.Admins.Find(id);
        }

        public void insertAdmin(Admin admin)
        {
            context.Admins.Add(admin);
        }

        public void deleteAdmin(int adminId)
        {
            Admin student = context.Admins.Find(adminId);
            context.Admins.Remove(student);
        }

        public void updateAdmin(Admin admin)
        {
            context.Entry(admin).State = EntityState.Modified;
        }

        /// <summary>
        /// Inventory
        /// </summary>
        /// <param name="entity"></param>
        public void updateInventory(InventoryEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void addItem(InventoryEntity inventory)
        {
            context.Inventory.Add(inventory);
            Save();
        }

        public void r_deleteItem(int item_id)
        {
            InventoryEntity item = context.Inventory.Find(item_id);
            context.Inventory.Remove(item);
            Save();
        }

        public void saveCalendar(Calendar calendar)
        {
            context.Calendar.Add(calendar);
            Save();
        }

        public void deleteCalendar(int? id)
        {
            Calendar item = context.Calendar.Find(id);
            context.Calendar.Remove(item);
            Save();

        
        }

        public void updateCalendar(Calendar entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }


        /// <summary>
        /// DB Context
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}