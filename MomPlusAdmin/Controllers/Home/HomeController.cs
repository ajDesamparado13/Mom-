using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Models;
using System.IO;
using System.Web.UI.WebControls;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Map;
using System.Data;
using MomPlusAdmin.Methods.AdminMethods;
using System.Web.Script.Serialization;
using MomPlusAdmin.Processes;
namespace MomPlusAdmin.MVC.Controllers.HomeController
{
     public class HomeController : Controller
    {

        AdminMethods adminController = new AdminMethods();
        MomPlusDbContext context = new MomPlusDbContext();
        // GET: /Home/

        #region Index and Login 
        public ActionResult Index()
        {
            if(Session["Login"] != null){

               return View("~/Views/Homepage/Homepage.cshtml");   
            }else{
                return View("Index");
            } 
            
        }
        
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Login(LoginModel adminmodel)
        {
               
                using (MomPlusDbContext db = new MomPlusDbContext())
                {
                    Admin admin = db.Admins.Where(a => a.username.Equals(adminmodel.Username) && a.password.Equals(adminmodel.Password)).FirstOrDefault();

                    if (admin != null)
                    {
                        Session["Login"] = admin.username.ToString();
                        return View("~/Views/Homepage/Homepage.cshtml");
                    }
                    else
                    {

                        ViewBag.LoginError = "Mismatch Username/Password";
                        return View("Index");
                    }

                }
        }
        #endregion
    }
        
}