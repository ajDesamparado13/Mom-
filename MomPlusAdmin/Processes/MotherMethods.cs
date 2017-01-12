using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Repository.MotherRepo;
using System.Web.Mvc;
using MomPlusAdmin.Models;

namespace MomPlusAdmin.Processes.AdminMethods
{
    public class MotherMethods : Controller
    {
        MotherRepo motherrepo = new MotherRepo();

       /// <summary>
       /// Mother Personal Information
       /// </summary>
       /// <param name="mother"></param>

       [HttpPost]
       public void CreateMotherAccount(Mother mother)
       {
           motherrepo.insertMother(mother);
           motherrepo.Save();
       }

       public void Edit(Mother mother)
       {
               motherrepo.updateMother(mother);
       }

       public void Delete(bool? saveChangesError = false, int id = 0)
       {
           if (saveChangesError.GetValueOrDefault())
           {
               ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
           }
           Mother mother = motherrepo.getMotherID(id);
       }

       public void Delete(int id)
       {
           Mother mother = motherrepo.getMotherID(id);
           motherrepo.deleteMother(id);
           motherrepo.Save();
       }

       protected override void Dispose(bool disposing)
       {
           motherrepo.Dispose();
           base.Dispose(disposing);
       }

        
    }
}