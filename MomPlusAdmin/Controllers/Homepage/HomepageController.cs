using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Models;
using MomPlusAdmin.Domain.Entity;
using System.IO;
using MomPlusAdmin.Methods.AdminMethods;
using System.Data;
using MomPlusAdmin.Domain.Context;
using Webdiyer.WebControls.Mvc;

namespace MomPlusAdmin.Controllers.Homepage
{
    
    public class HomepageController : Controller
    {
        MomPlusDbContext context = new MomPlusDbContext();

        public ActionResult Homepage()
        {
            if(Session["Login"]==null){
                 return RedirectToAction("Index", "Home");
            }else{
                return View();
            }
            
        }
        #region signout
        public ActionResult Signout() {
            Session["Login"] = null;
          return RedirectToAction("Index","Home");
        
        }
        #endregion

        public PartialViewResult EventParticipants()
        {
            var _event = context.Calendar.ToList();
            return PartialView(_event);
        }

      
    } 
}