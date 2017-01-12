using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Processes.AdminMethods;
using MomPlusAdmin.Models;
using System.Data;
using System.Web.Script.Serialization;

namespace MomPlusAdmin.Controllers.MotherProfile
{
    public class MotherProfileController : Controller
    {
         MomPlusDbContext db = new MomPlusDbContext();
           
        #region MotherProfile
        public ActionResult getMotherProfile(Guid id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.motherID = id;
                PersonalInformation(id);
                return View();
            }
        }
        public PartialViewResult PersonalInformation(Guid id){

            Mother mother = db.Mother.Find(id);
            var today = DateTime.Today;
            var age = today.Year - mother.birth_date.Year;
            var model = new MotherModel() 
            {
                id = mother.mother_id,
                MotherFName = mother.first_name,
                MotherLname = mother.last_name,
                MotherAddress = mother.address,
                MotherCnum = mother.contact_num,
                MotherBdate = mother.birth_date,
                LMP = mother.LMP,
                EDC = mother.EDC,
                DateRegistered = mother.date_registered,
                motherUsername = mother.username,
                motherPassword = mother.password,
                MotherAge = age,
                
            };

            return PartialView(model);
             
        }
        [HttpPost]
        [ActionName("getMotherProfile")]
        public ActionResult updateMother(MotherModel mothermodel, Guid id) {
            MotherMethods methods = new MotherMethods();

            var tempEDC = mothermodel.LMP.AddMonths(9);
            ViewBag.motherID = id;
            try
            {
                Mother motherEntity = new Mother()
                {
                    mother_id = id,
                    first_name = mothermodel.MotherFName,
                    last_name = mothermodel.MotherLname,
                    address = mothermodel.MotherAddress,
                    birth_date = mothermodel.MotherBdate,
                    contact_num = mothermodel.MotherCnum,
                    username = mothermodel.motherUsername,
                    password = mothermodel.motherPassword,
                    LMP = mothermodel.LMP,
                    EDC = tempEDC,
                    date_registered = mothermodel.DateRegistered,
                   

                };
                if (ModelState.IsValid)
                {

                    methods.Edit(motherEntity);
                    updateJson();
                    ViewBag.isUpdated = "Account updated successfully.";
                }
                else {
                    ViewBag.notUpdated = "Opps, Something went wrong, please make sure to provide valid input.!";
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError(string.Empty, "Opps, Something went wrong, please make sure to provide valid input.!");
            }
                
                return PartialView("getMotherProfile");
           
        
        }
        #endregion

        public void updateJson()
        {
            string path = Server.MapPath("~/App_Data/Mother.json");
            System.IO.File.Delete(path);

            var jsonmother = db.Mother.ToList();

            string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
            string savePath = Server.MapPath("~/App_Data/");
            System.IO.File.WriteAllText(savePath + "Mother.json", jsondata);
        }
    }
}
