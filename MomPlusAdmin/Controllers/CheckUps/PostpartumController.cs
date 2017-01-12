using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Repository.MotherRepo;
using MomPlusAdmin.Models;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Processes;
using System.Web.Script.Serialization;

namespace MomPlusAdmin.Controllers.CheckUps
{
    public class PostpartumController : Controller
    {
        MotherRepo repo = new MotherRepo();
        MomPlusDbContext context = new MomPlusDbContext();
        #region session
        public bool isUserLogin()
        {
            if (Session["Login"] != null)
            {
                return true;
            }
            return false;
        }

        #endregion


        #region create postpartum
        public ActionResult createPostPartum(Guid id)
        {

            if (isUserLogin())
            {
                ViewBag.motherID = id;
                return View();
            }
             TempData["sessionIsExpired"] = "Your session has expired.";
             return RedirectToAction("Index", "Home");
       }
        [HttpPost]
        public ActionResult createPostPartum(PostPartumModel model, Guid id)
        {
            
            MedPersonnelMethod method = new MedPersonnelMethod();
            var admin_username = Session["Login"];
            if (isUserLogin())
            {
                if (ModelState.IsValid)
                {

                    PostPartumEntity entity = new PostPartumEntity()
                    {

                        mother_id = id,
                        within24hrs_after_delivery = model.within24hrs_after_delivery,
                        within1week_after_delivery = model.within1week_after_delivery,
                        dateAndTime_initiated_breastfeeding = model.dateAndTime_initiated_breastfeeding,
                        date_iron_was_given = model.date_iron_was_given,
                        no_of_iron_was_given = model.no_of_iron_was_given,
                        date_vitA_was_given = model.date_vitaminA_was_given,
                    };
                    repo.addPostpartum(entity);
                    method.updatePostpartum(entity, admin_username.ToString());
                    var jsonmother = context.Postpartum.ToList();
                    string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
                    string path = Server.MapPath("~/App_Data/");
                    System.IO.File.WriteAllText(path + "Postpartum.json", jsondata);

                    TempData["isSuccessful"] = 1;
                    return RedirectToAction("getMotherProfile", "MotherProfile", new { id = id });
                }
                ViewBag.motherID = id;
                return View();
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region update postpartum
        public ActionResult updatePostpartum(Guid id)
        {

            if (isUserLogin())
            {

                PostPartumEntity postpartum = context.Postpartum.Where(a => a.mother_id == id).OrderByDescending(a => a.postpartum_id).First();
                PostPartumModel model = new PostPartumModel()
                {
                    postpartumID = postpartum.postpartum_id,
                    motherID = postpartum.mother_id,
                    within24hrs_after_delivery = postpartum.within24hrs_after_delivery,
                    within1week_after_delivery = postpartum.within1week_after_delivery,
                    dateAndTime_initiated_breastfeeding = postpartum.dateAndTime_initiated_breastfeeding,
                    date_iron_was_given = postpartum.date_iron_was_given,
                    no_of_iron_was_given = postpartum.no_of_iron_was_given,
                    date_vitaminA_was_given = postpartum.date_vitA_was_given,
                };
                return View(model);
            }
             TempData["sessionIsExpired"] = "Your session has expired.";
             return RedirectToAction("Index", "Home");
            
       }

        [HttpPost]
        public ActionResult updatePostpartum(PostPartumModel models, int id)
        {
            MedPersonnelMethod method = new MedPersonnelMethod();
            var admin_username = Session["Login"];

            if (isUserLogin())
            {
                if (ModelState.IsValid)
                {
                    PostPartumEntity entity = new PostPartumEntity()
                    {
                        postpartum_id = id,
                        mother_id = models.motherID,

                        within24hrs_after_delivery = models.within24hrs_after_delivery,
                        within1week_after_delivery = models.within1week_after_delivery,
                        dateAndTime_initiated_breastfeeding = models.dateAndTime_initiated_breastfeeding,
                        date_iron_was_given = models.date_iron_was_given,
                        no_of_iron_was_given = models.no_of_iron_was_given,
                        date_vitA_was_given = models.date_vitaminA_was_given,
                    };
                    repo.updatePostpartum(entity);
                    method.updatePostpartum(entity, admin_username.ToString());
                    updateJson();
                    TempData["isSuccessful"] = 1;
                    return RedirectToAction("getMotherProfile", "MotherProfile", new { id = models.motherID });

                }

                return View();
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region display postpartum records
        public ActionResult displayPostpartumRecords(Guid id)
        {
            if (isUserLogin())
            {
                ViewBag.motherID = id;

                var results = context.Postpartum.Where(e => e.mother_id.Equals(id)).ToList();
                return PartialView(results);
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
       }
        #endregion

        #region update json

        public void updateJson()
        {
            string path = Server.MapPath("~/App_Data/Postpartum.json");
            System.IO.File.Delete(path);

            var jsonmother = context.Postpartum.ToList();

            string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
            string savePath = Server.MapPath("~/App_Data/");
            System.IO.File.WriteAllText(savePath + "Postpartum.json", jsondata);
        }

        #endregion

    }
}
