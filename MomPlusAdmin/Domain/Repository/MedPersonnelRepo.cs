using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Context;
using System.Data;
using System.Web.Http;

namespace MomPlusAdmin.Domain.Repository
{
    public class MedPersonnelRepo
    {
        private MomPlusDbContext context = new MomPlusDbContext();

        
        public void AddPersonnel(MedPersonnelHistoryEntity entity)
        {
            context.personnel.Add(entity);
            context.SaveChanges();
        }

        public void updatePersonnel(MedPersonnelHistoryEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}