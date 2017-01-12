using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Models;
using MomPlusAdmin.Domain.Repository.MotherRepo;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Repository.BabyRepo;
using System.Web.Helpers;
using System.Data.Entity;
using System.Collections;
using MomPlusAdmin.Processes;
using System.Web.Script.Serialization;

namespace MomPlusAdmin.Controllers.CheckUps
{
    public class BabyController : Controller
    {
        BabyRepo repo = new BabyRepo();
        MomPlusDbContext context = new MomPlusDbContext();
        MedPersonnelMethod medsHistory = new MedPersonnelMethod();

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

        #region baby
        public ActionResult RegisterBaby(Guid id)
        {
            if(isUserLogin())
            {
            ViewBag.motherID = id;
            return View();
            }
             TempData["sessionIsExpired"] = "Your session has expired.";
             return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult RegisterBaby(BabyModel _model, Guid id)
        {

            if (isUserLogin())
            {
                var admin_username = Session["Login"];
                Mother mother = context.Mother.Find(id);

                if (ModelState.IsValid)
                {
                    BabyEntity baby = new BabyEntity()
                    {
                        mother_id = id,

                        first_name = _model.FirstName,
                        last_name = _model.LastName,
                        gender = _model.Gender,
                        weight = Convert.ToDecimal(_model.Weight),
                        height = _model.Height,
                        date_registered = _model.DateRegister,
                        birth_date = _model.BDate,
                    };
                    repo.registerBaby(baby);

                    BabyEntity entity = context.Baby.Where(i => i.mother_id.Equals(id) && i.first_name.Equals(_model.FirstName)).FirstOrDefault();
                    addBabyDev(entity.baby_id, _model.Weight, _model.Height);


                    var jsonmother = context.Baby.ToList();
                    string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
                    string path = Server.MapPath("~/App_Data/");
                    System.IO.File.WriteAllText(path + "Baby.json", jsondata);

                    TempData["newBaby"] = 1;
                    return RedirectToAction("getMotherProfile", "MotherProfile", new { id = id });
                }
                ViewBag.babyID = id;
                return View();
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }


        public ActionResult displayListOfBaby(Guid id)
        {
            if (isUserLogin())
            {

                ViewBag.motherID = id;
                var results = context.Baby.Where(e => e.mother_id.Equals(id)).ToList();
                return PartialView(results);
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult BabyProfile(Guid id)
        {
            if (isUserLogin())
            {
                BabyEntity baby = context.Baby.Where(b => b.baby_id.Equals(id)).FirstOrDefault();

                BabyModel model = new BabyModel()
                {
                    mother_id = baby.mother_id,
                    baby_id = id,
                    FirstName = baby.first_name,
                    LastName = baby.last_name,
                    BDate = baby.birth_date,
                    Gender = baby.gender,
                    Weight = Convert.ToDouble(baby.weight),
                    Height = baby.height,
                    DateRegister = baby.date_registered,
                };
                ViewBag.babyId = model.baby_id;
                ViewBag.motherID = model.mother_id;
                return View(model);
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult BabyProfile(BabyModel model, Guid id)
        {
            var admin_username = Session["Login"];
            var m = model.mother_id;
            Mother mother = context.Mother.Find(model.mother_id);
            if (ModelState.IsValid)
            {
                BabyEntity entity = new BabyEntity()
                {
                    mother_id = model.mother_id,
                    baby_id = id,

                    first_name = model.FirstName,
                    last_name = model.LastName,
                    weight = Convert.ToDecimal(model.Weight),
                    height = model.Height,
                    birth_date = model.BDate,
                    gender = model.Gender,
                    date_registered = model.DateRegister,
                   
                };
                repo.updateBabyProfile(entity);
                updateBaby(id, Convert.ToDouble(entity.weight), entity.height, entity.birth_date);
                updateJsonBaby();
                return RedirectToAction("BabyProfile");
            }
            ViewBag.motherID = id;
            return View();
        }

        #endregion

        #region baby dev

        public void addBabyDev(Guid id, Double weight, int height)
        {
            var bmi = weight/height;
            BabyDev dev = new BabyDev() 
            {
                baby_id = id,
                first_month = Convert.ToDecimal(bmi),
            };
            repo.addBMI(dev);
        }
        public void updateBaby(Guid id, Double weight, int height, DateTime bdate)
        {
            BabyMethod method = new BabyMethod();
            var dev = method.checkBMI(id, weight,height, bdate);

            repo.updateBMI(dev);
        }



        public ActionResult getDev(Guid id)
        {

            var results = (from c in context.BabyDev where c.baby_id == id select c);
            ArrayList _xValue = new ArrayList();
            ArrayList yValues_ = new ArrayList();

              foreach(var  item in results){
               yValues_.Add(item.first_month);
               yValues_.Add(item.second_month);
               yValues_.Add(item.third_month);
               yValues_.Add(item.fourth_month);
               yValues_.Add(item.fift_month);
               yValues_.Add(item.six_month);
               yValues_.Add(item.seven_month);
               yValues_.Add(item.eight_month);
               yValues_.Add(item.nine_month);
               yValues_.Add(item.ten_month);
               yValues_.Add(item.eleven_month);
               yValues_.Add(item.twelve_month);
              }

                
            var chart = new Chart(width: 800, height: 300, theme: ChartTheme.Vanilla)
                .AddTitle("Baby's development From 1st to 12th month")
                .AddLegend("Legend")
                .AddSeries(chartType: "line", name: "BMI",
                xValue: new[]{"1st","2nd", "3rd", "4th", "5th","6th", "7th", "8th", "9th","10th", "11th","12th"},
                yValues: yValues_.ToArray())
            .GetBytes("png");
            return File(chart, "image/bytes");
        }
        #endregion

        #region Immunization


        public ActionResult addImmunization(Guid id)
        {
            if(isUserLogin())
            {
            ViewBag.babyID = id;
            return View();
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
             return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult addImmunization(ImmunizationModel model, Guid id)
        {
            var admin_username = Session["Login"];

            if (isUserLogin())
            {
                if (ModelState.IsValid)
                {
                    ImmunizationEntity entity = new ImmunizationEntity()
                    {
                        baby_id = id,
                        immunization_date = model.ImmunizationDate,
                        six_to_twelvemos = model.six_to_twelvemos,
                        twelve_to_59mos = model.twelve_to_59mos,
                        twelve59mos_dosage1 = model.twelve59mos_dosage1,
                        twelve59mos_dosage2 = model.twelve59mos_dosage2,

                    };
                    repo.AddImmunization(entity);
                    BabyEntity getBabyID = context.Baby.Where(i => i.baby_id.Equals(id)).FirstOrDefault();
                    medsHistory.updateImmunzation(entity, getBabyID.mother_id, admin_username.ToString());


                    var jsonmother = context.Immunization.ToList();
                    string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
                    string path = Server.MapPath("~/App_Data/");
                    System.IO.File.WriteAllText(path + "Immunization.json", jsondata);

                    TempData["ImmunizationRecord"] = 1;
                    return RedirectToAction("BabyProfile", "Baby", new { id = id });
                }
                ViewBag.babyID = id;
                return View();
            }
            TempData["sessionIsExpired"] = "Your session has expired.";
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult displayImmunizationRecords(Guid id)
        {
            ViewBag.babyID = id;
           
            var results = context.Immunization.Where(e => e.baby_id.Equals(id)).ToList();
            return PartialView(results);
        }

        public ActionResult updateImmunization(Guid id) // id = babyid
        {
            
            
            ImmunizationEntity results = context.Immunization.Where(a => a.baby_id == id).OrderByDescending(a => a.immunization_id).First();
           
            ImmunizationModel model = new ImmunizationModel()
            {
               immunization_id = results.immunization_id,
               baby_id = results.baby_id,
               ImmunizationDate = results.immunization_date,
               six_to_twelvemos = results.six_to_twelvemos,
               twelve_to_59mos = results.twelve_to_59mos,
               twelve59mos_dosage1 = results.twelve59mos_dosage1,
               twelve59mos_dosage2 = results.twelve59mos_dosage2,
               
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult updateImmunization(ImmunizationModel models)
        {

            var admin_username = Session["Login"];
            if (ModelState.IsValid)
            {
                ImmunizationEntity entity = new ImmunizationEntity()
                {
                    immunization_id = Convert.ToInt32(models.immunization_id),
                    baby_id = models.baby_id,

                    immunization_date = models.ImmunizationDate,
                    six_to_twelvemos = models.six_to_twelvemos,
                    twelve_to_59mos = models.twelve_to_59mos,
                    twelve59mos_dosage1 = models.twelve59mos_dosage1,
                    twelve59mos_dosage2 = models.twelve59mos_dosage2,

                };
                repo.updateImmunization(entity);

                BabyEntity getBabyID = context.Baby.Where(i => i.baby_id.Equals(models.baby_id)).FirstOrDefault();
                medsHistory.updateImmunzation(entity, getBabyID.mother_id, admin_username.ToString());
                updateJsonImmunization();

                TempData["ImmunizationRecord"] = "1";
                return RedirectToAction("BabyProfile", "Baby", new { id = models.baby_id });


            }
            return View();
        }
        #endregion
        #region update json

        public void updateJsonBaby()
        {
            string path = Server.MapPath("~/App_Data/Baby.json");
            System.IO.File.Delete(path);

            var jsonmother = context.Baby.ToList();

            string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
            string savePath = Server.MapPath("~/App_Data/");
            System.IO.File.WriteAllText(savePath + "Baby.json", jsondata);
        }

        public void updateJsonImmunization()
        {
            string path = Server.MapPath("~/App_Data/Immunization.json");
            System.IO.File.Delete(path);

            var jsonmother = context.Immunization.ToList();

            string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
            string savePath = Server.MapPath("~/App_Data/");
            System.IO.File.WriteAllText(savePath + "Immunization.json", jsondata);
        }


        #endregion

    }
}
