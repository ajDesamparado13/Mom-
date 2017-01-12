using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Models;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Repository.MotherRepo;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Processes;
using System.Collections.ObjectModel;
using MomPlusAdmin.Processes.AdminMethods;
using System.Web.Script.Serialization;

namespace MomPlusAdmin.Controllers.CheckUps
{
    public class PrenatalController : Controller
    {
        MotherRepo repo = new MotherRepo();
        MomPlusDbContext context = new MomPlusDbContext();
        MedPersonnelMethod personnel = new MedPersonnelMethod();

        #region Session check
        public bool isUserLogin()
        {
            if (Session["Login"] != null)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Create Prenatal
        public ActionResult CreatePrenatalRecord(Guid id) {

            ViewBag.motherID = id;
            if (isUserLogin())
            {
                return View();
            }
            else
            {
             TempData["sessionIsExpired"] = "Your session has expired.";
             return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreatePrenatalRecord(PrenatalModel model, Guid id) {

            if (isUserLogin())
            {
                PrenatalEntity prenatal = new PrenatalEntity();
                var admin_username = Session["Login"];

                switch (model.TetaType)
                {
                    case "TT1": prenatal.tt1 = model.Toxiod; break;
                    case "TT2": prenatal.tt2 = model.Toxiod; break;
                    case "TT3": prenatal.tt3 = model.Toxiod; break;
                    case "TT4": prenatal.tt4 = model.Toxiod; break;
                    case "TT5": prenatal.tt5 = model.Toxiod; break;
                }

                switch (model.TrimesterNo)
                {
                    case "First Trimester": prenatal.first_trimester = model.Trimester; break;
                    case "Second Trimester": prenatal.second_trimester = model.Trimester; break;
                    case "Third Trimester": prenatal.third_trimester = model.Trimester; break;
                }

                if (ModelState.IsValid)
                {

                    PrenatalEntity prenatalentity = new PrenatalEntity()
                    {

                        mother_id = id,

                        tt1 = prenatal.tt1,
                        tt2 = prenatal.tt2,
                        tt3 = prenatal.tt3,
                        tt4 = prenatal.tt4,
                        tt5 = prenatal.tt5,

                        date_given_vit_A = model.DateGivenVitA,
                        date_iron_was_given = model.DateIronWasGiven,
                        no_iron_was_given = model.NoIronWasGiven,

                        risk_code_detected = model.RiskCodeDetected,
                        date_terminated = model.DateTerminated,
                        outcome = model.OutCome,
                        birth_weight = Convert.ToDecimal(model.BirthWeight),
                        place_Of_delivery = model.PlaceOfDelivery,
                        attended_B = model.AttendedB,
                        remarks = model.Remarks,

                        first_trimester = prenatal.first_trimester,
                        second_trimester = prenatal.second_trimester,
                        third_trimester = prenatal.third_trimester,

                    };
                    repo.addPrenatal(prenatalentity);
                    personnel.CheckInputsPrenatal(prenatalentity, admin_username.ToString());
                    
                    var jsonmother = context.Prenatal.ToList();
                    
                    string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
                    string path = Server.MapPath("~/App_Data/");
                    System.IO.File.WriteAllText(path + "Prenatal.json", jsondata);
                    
                    TempData["isSuccessful"] = 1;
                    return RedirectToAction("getMotherProfile", "MotherProfile", new { id = id });
                }
                ViewBag.motherID = id;
                TempData["unSuccessful"] = 1;
                return View();
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Display Prental Record

        public PartialViewResult PrenatalRecords(Guid id)
        {
            ViewBag.motherID = id;
            if (isUserLogin())
            {
                var mother = context.Mother.Find(id);

                var results = context.Prenatal.Where(e => e.mother_id.Equals(id)).ToList();
                return PartialView(results);
            }
            else
            {
                TempData["sessionIsExpired"] = "Your session has expired.";
                return PartialView("~/Views/Home/Index.cshtml"); 
            }
        }

       
        #endregion

        #region Add TTR and Trimester

        public ActionResult addTTR_and_Trimester(Guid id)
        {

            if (isUserLogin())
            {

                PrenatalEntity results = context.Prenatal.Where(a => a.mother_id == id).OrderByDescending(a => a.prenatal_id).First();

                PrenatalModel model = new PrenatalModel()
                {
                    prenatalID = results.prenatal_id,
                    motherID = results.mother_id,

                    tt1 = results.tt1,
                    tt2 = results.tt2,
                    tt3 = results.tt3,
                    tt4 = results.tt4,
                    tt5 = results.tt5,

                    DateGivenVitA = results.date_given_vit_A,
                    DateIronWasGiven = results.date_iron_was_given,
                    NoIronWasGiven = results.no_iron_was_given,
                    RiskCodeDetected = results.risk_code_detected,
                    DateTerminated = results.date_terminated,
                    OutCome = results.outcome,
                    BirthWeight = Convert.ToDouble(results.birth_weight),
                    PlaceOfDelivery = results.place_Of_delivery,
                    AttendedB = results.attended_B,
                    Remarks = results.remarks,

                    first_trimester = results.first_trimester,
                    second_trimester = results.second_trimester,
                    third_trimester = results.third_trimester,

                };
                return View(model);
            }
             TempData["sessionIsExpired"] = "Your session has expired.";
             return RedirectToAction("Index", "Home");
         }
        [HttpPost]
        public ActionResult addTTR_and_Trimester(PrenatalModel model)
        {

            if (isUserLogin())
            {
                var admin_username = Session["Login"];
                if (ModelState.IsValid)
                {
                    PrenatalEntity entity = new PrenatalEntity()
                    {
                        prenatal_id = model.prenatalID,
                        mother_id = model.motherID,

                        tt1 = model.tt1,
                        tt2 = model.tt2,
                        tt3 = model.tt3,
                        tt4 = model.tt4,
                        tt5 = model.tt5,

                        date_given_vit_A = model.DateGivenVitA,
                        date_iron_was_given = model.DateIronWasGiven,
                        no_iron_was_given = model.NoIronWasGiven,

                        risk_code_detected = model.RiskCodeDetected,
                        date_terminated = model.DateTerminated,
                        outcome = model.OutCome,
                        birth_weight = Convert.ToDecimal(model.BirthWeight),
                        place_Of_delivery = model.PlaceOfDelivery,
                        attended_B = model.AttendedB,
                        remarks = model.Remarks,

                        first_trimester = model.first_trimester,
                        second_trimester = model.second_trimester,
                        third_trimester = model.third_trimester,
                    };
                    repo.updatePrenatal(entity);
                    personnel.updatePrenatalMeds(entity, admin_username.ToString());
                    updateJson();

                    TempData["isSuccessful"] = 1;
                    return RedirectToAction("getMotherProfile", "MotherProfile", new { id = model.motherID });
                }
                TempData["unSuccessful"] = 1;
                return View(model);
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region update Json
        public void updateJson()
        {
            string path = Server.MapPath("~/App_Data/Prenatal.json");
            System.IO.File.Delete(path);

            var jsonmother = context.Prenatal.ToList();

            string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
            string savePath = Server.MapPath("~/App_Data/");
            System.IO.File.WriteAllText(savePath + "Prenatal.json", jsondata);
        }

        #endregion
    }
}
