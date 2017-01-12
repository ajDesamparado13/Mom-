using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Repository.AdminRepo;

namespace MomPlusAdmin.Methods.AdminMethods
{
        public class AdminMethods : Controller
        {
            AdminRepo rAdmin = new AdminRepo();
           [HttpPost]
            public void CreateAdminAccount(Admin admin)
            {
                rAdmin.insertAdmin(admin);
                rAdmin.Save();
            }
            public void Edit(int id)
            {
                Admin admin = rAdmin.getAdminByID(id);
            }
            
            public void Edit(Admin admin)
            {

                if (ModelState.IsValid)
                {
                    rAdmin.updateAdmin(admin);
                    rAdmin.Save();
                }
             }

            public void Delete(bool? saveChangesError = false, int id = 0)
            {
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
                }
                Admin admin = rAdmin.getAdminByID(id);
            }
            
            public void Delete(int id)
            {
                Admin admin = rAdmin.getAdminByID(id);
                rAdmin.deleteAdmin(id);
                rAdmin.Save();
            }

            protected override void Dispose(bool disposing)
            {
                rAdmin.Dispose();
                base.Dispose(disposing);
            }

        }
    }

