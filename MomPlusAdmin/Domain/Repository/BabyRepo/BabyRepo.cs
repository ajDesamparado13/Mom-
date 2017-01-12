using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using System.Data;

namespace MomPlusAdmin.Domain.Repository.BabyRepo
{
    public class BabyRepo
    {
        MomPlusDbContext context = new MomPlusDbContext();
        
        #region Create and update baby profile
        public void registerBaby(BabyEntity baby)
        {
            context.Baby.Add(baby);
            Save();
        }

        public void updateBabyProfile(BabyEntity baby)
        {
            context.Entry(baby).State = EntityState.Modified;
            Save();
        }

        public void addBMI(BabyDev baby)
        {
            context.BabyDev.Add(baby);
            Save();
        }

        public void updateBMI(BabyDev baby)
        {
            context.Entry(baby).State = EntityState.Modified;
            Save();
        }

        #endregion


        #region Immunization

        public void AddImmunization(ImmunizationEntity immunization)
        {
            context.Immunization.Add(immunization);
            Save();
        }

        public void updateImmunization(ImmunizationEntity immunization)
        {
            context.Entry(immunization).State = EntityState.Modified;
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