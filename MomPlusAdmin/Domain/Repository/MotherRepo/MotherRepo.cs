using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using System.Data;
using MomPlusAdmin.Models;

namespace MomPlusAdmin.Domain.Repository.MotherRepo
{
    public class MotherRepo
    {
        MomPlusDbContext context = new MomPlusDbContext();


        public MotherRepo() { }

        #region Mother Profile
        public IEnumerable<Mother> getMother()
        {
            return context.Mother.ToList();
        }

        public Mother getMotherID(int id)
        {
            return context.Mother.Find(id);
                
        }

        public void insertMother(Mother mother)
        {
            context.Mother.Add(mother);
        }

        public void deleteMother(int motherId)
        {
            Mother mother = context.Mother.Find(motherId);
            context.Mother.Remove(mother);
            Dispose();
        }

        public void updateMother(Mother mother)
        {
                context.Entry(mother).State = EntityState.Modified;
                Save();
         }
        #endregion

        #region Prenatal

        public void addPrenatal(PrenatalEntity prenatal)
        {
            context.Prenatal.Add(prenatal);
            Save();
        }

        public void updatePrenatal(PrenatalEntity prenatal)
        {
            context.Entry(prenatal).State = EntityState.Modified;
            Save();
        }

        #endregion

        #region Postpartum

        public void addPostpartum(PostPartumEntity postpartum)
        {
            context.Postpartum.Add(postpartum);
            Save();
        }

        public void updatePostpartum(PostPartumEntity postpartum)
        {
            context.Entry(postpartum).State = EntityState.Modified;
            Save();
        }

        #endregion

        #region dbContext
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
        #endregion
}

}