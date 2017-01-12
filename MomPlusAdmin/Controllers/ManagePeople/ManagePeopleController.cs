using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Methods.AdminMethods;
using MomPlusAdmin.Models;
using MomPlusAdmin.Domain.Entity;
using System.Data;
using MomPlusAdmin.Processes.AdminMethods;
using MomPlusAdmin.Processes;
using MomPlusAdmin.Domain.Repository;
using System.Web.Script.Serialization;

namespace MomPlusAdmin.Controllers.Manage_People
{
    public class ManagePeopleController : Controller
    {
        MomPlusDbContext list = new MomPlusDbContext();
        #region Admin List
        public ActionResult AdminList(String sortOrder)
        {

            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
                ViewBag.LNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
                ViewBag.AddressSortParm = String.IsNullOrEmpty(sortOrder) ? "address" : "";
                ViewBag.CnumSortParm = String.IsNullOrEmpty(sortOrder) ? "cnum" : "";
                ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "username" : "";
                ViewBag.PasswordSortParm = String.IsNullOrEmpty(sortOrder) ? "password" : "";
                ViewBag.PositionSortParm = String.IsNullOrEmpty(sortOrder) ? "position" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                var admin = from s in list.Admins
                            select s;


                switch (sortOrder)
                {
                    case "fname_desc":
                        admin = admin.OrderByDescending(s => s.first_name);
                        break;
                    case "lname_desc":
                        admin = admin.OrderByDescending(s => s.last_name);
                        break;

                    case "address":
                        admin = admin.OrderByDescending(s => s.address);
                        break;

                    case "cnum":
                        admin = admin.OrderByDescending(s => s.contact_num);
                        break;

                    case "username":
                        admin = admin.OrderByDescending(s => s.username);
                        break;

                    case "password":
                        admin = admin.OrderByDescending(s => s.password);
                        break;

                    case "position":
                        admin = admin.OrderByDescending(s => s.Position);
                        break;
                    case "Date":
                        admin = admin.OrderBy(s => s.birth_date);
                        break;
                    default:
                        admin = admin.OrderBy(s => s.last_name);
                        break;
                }
                return View(admin.ToList());

            }

        }

        public ActionResult searchAdmin(String SearchString)
        {

            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Admin admin = list.Admins.Where(i => i.first_name.Contains(SearchString)
                            || i.last_name.Contains(SearchString)).FirstOrDefault();



                if (admin != null)
                {
                    var searchAdmin = list.Admins.Where(i => i.admin_id == admin.admin_id);
                    return View("AdminList", searchAdmin.ToList());
                }
                else
                {
                    TempData["notFound"] = "Search not found";
                }
                var all = from a in list.Admins select a;
                return View("AdminList", all.ToList());

            }
        }
        #endregion

        #region Add admin account
        public ActionResult AdminRegistration()
        {
            //if (Session["Login"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            return View();
            //}   
        }


        [HttpPost]
        public ActionResult AdminRegistration(AdminModel adminmodel)
        {
            AdminMethods adminController = new AdminMethods();
            // get age
            var today = DateTime.Today;
            var age = today.Year - adminmodel.Bday.Year;

            try
            {
                if (ModelState.IsValid)
                {
                    if (age >= 18 && age <= 100)
                    {

                        // Pass inputs to Entity for the insertion
                        var username = list.Admins.Any(a => a.username.Equals(adminmodel.Username));
                        var cnum = list.Admins.Any(a => a.contact_num.Equals(adminmodel.ContactNum));

                        if (username)
                        {
                            ViewBag.usernameExist = "Username already taken, provide new one.";
                        }
                        if (cnum)
                        {
                            ViewBag.cnumExist = "Contact Number already exist, provide new one.";
                        }
                        else
                        {
                            Admin admin = new Admin()
                            {

                                first_name = adminmodel.Fname,
                                last_name = adminmodel.LName,
                                address = adminmodel.Address,
                                birth_date = adminmodel.Bday,
                                contact_num = adminmodel.ContactNum,
                                gender = adminmodel.Gender,
                                username = adminmodel.Username,
                                password = adminmodel.Password,
                                Position = adminmodel.Position,
                                Clinic = adminmodel.Clinic,
                            };

                            adminController.CreateAdminAccount(admin);
                            ViewBag.newEntry = "New entry added";
                            TempData["isSuccessful"] = 1;
                            return View();

                        }

                    }
                    else
                    {
                        TempData["unSuccessful"] = 1;
                        ViewBag.notValidBday = "Please make sure you are 18 years old or above.";
                    }

                }
            }
            catch (DataException /* dex */)
            {

                ModelState.AddModelError(string.Empty, "Opps, Something went wrong!");
            }

            TempData["unSuccessful"] = 1;
            return View();

        }
        #endregion

        #region Personnel
        public PartialViewResult getPersonnel(Guid id)
        {
            var personnel = list.personnel.Where(p => p.user_id.Equals(id)).ToList();
            return PartialView(personnel);
        }


        #endregion

        //Summary: Mother List and Registration
        #region Mother Registration

        public ActionResult MotherRegistration()
        {
            if (Session["Login"] == null)
            {
                TempData["sessionIsExpired"] = "Your session has expired.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult MotherRegistration(MotherModel mothermodel)
        {
            MotherMethods motherController = new MotherMethods();
            MedPersonnelRepo repo = new MedPersonnelRepo();
            //personnel who created the account

            var adminUsername = Session["Login"];

            // get age
            var today = DateTime.Today;
            var Age = today.Year - mothermodel.MotherBdate.Year;

            //Get date registered
            DateTime dateNow = DateTime.Now.ToLocalTime();


            // get EDC
            var tempLMP = mothermodel.LMP;
            var tempEDC = tempLMP.AddMonths(9);


            try
            {
                if (ModelState.IsValid)
                {
                    if (Age >= 12 && Age <= 60)
                    {

                        // Pass inputs to Entity for the insertion
                        var username = list.Mother.Any(a => a.username == mothermodel.motherUsername);
                        var cnum = list.Mother.Any(a => a.contact_num == mothermodel.MotherCnum);

                        if (username)
                        {
                            TempData["unSuccessful"] = 1;
                            ViewBag.usernameExist = "Username already taken, provide new one.";
                        }
                        if (cnum)
                        {
                            TempData["unSuccessful"] = 1;
                            ViewBag.cnumExist = "Contact Number already exist, provide new one.";
                        }
                        else
                        {

                            Mother mother = new Mother()
                            {

                                first_name = mothermodel.MotherFName,
                                last_name = mothermodel.MotherLname,
                                address = mothermodel.MotherAddress,
                                birth_date = mothermodel.MotherBdate,
                                contact_num = mothermodel.MotherCnum,
                                username = mothermodel.motherUsername,
                                password = mothermodel.motherPassword,
                                LMP = mothermodel.LMP,
                                EDC = tempEDC,
                                date_registered = dateNow,

                            };

                            motherController.CreateMotherAccount(mother);

                            var jsonmother = list.Mother.ToList();
                            string jsondata = new JavaScriptSerializer().Serialize(jsonmother);
                            string path = Server.MapPath("~/App_Data/");
                            System.IO.File.WriteAllText(path + "Mother.json", jsondata);

                            ViewBag.EDC = tempEDC.ToShortDateString();
                            ViewBag.newEntry = "New entry added";
                            return View();
                        }
                    }

                }
                else
                {
                    TempData["unSuccessful"] = 1;
                    ViewBag.notValidBday = "Please add valid age.";
                }
            }
            catch (DataException /* dex */)
            {

                ModelState.AddModelError(string.Empty, "Opps, Something went wrong!");
            }


            return View();

        }


        #endregion

        #region Mother List

        public ActionResult MotherList(String sortOrder)
        {

            if (Session["Login"] == null)
            {
                TempData["sessionIsExpired"] = "Your session has expired.";
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
                ViewBag.LNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
                ViewBag.AddressSortParm = String.IsNullOrEmpty(sortOrder) ? "address" : "";
                ViewBag.CnumSortParm = String.IsNullOrEmpty(sortOrder) ? "cnum" : "";
                ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "username" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                var mother = from s in list.Mother
                             select s;
                switch (sortOrder)
                {
                    case "fname_desc":
                        mother = mother.OrderByDescending(s => s.first_name);
                        break;
                    case "lname_desc":
                        mother = mother.OrderByDescending(s => s.last_name);
                        break;

                    case "address":
                        mother = mother.OrderByDescending(s => s.address);
                        break;

                    case "cnum":
                        mother = mother.OrderByDescending(s => s.contact_num);
                        break;

                    case "username":
                        mother = mother.OrderByDescending(s => s.username);
                        break;

                    case "Date":
                        mother = mother.OrderBy(s => s.birth_date);
                        break;
                    default:
                        mother = mother.OrderBy(s => s.last_name);
                        break;
                }
                return View(mother.ToList());

            }

        }

        public ActionResult searchMother(String SearchString)
        {

            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Mother mother = list.Mother.Where(i => i.first_name.Contains(SearchString)
                           || i.last_name.Contains(SearchString)).FirstOrDefault();



                if (mother != null)
                {
                    var searchMother = list.Mother.Where(i => i.mother_id == mother.mother_id);
                    return View("MotherList", searchMother.ToList());
                }
                else
                {
                    TempData["notFound"] = "Search not found";
                }
                var all = from a in list.Mother select a;
                return View("MotherList", all.ToList());

            }
        }


        #endregion

        #region postpartum list

        public PartialViewResult displayPostpartum(String sortOrder)
        {

            ViewBag.FirstPP = sortOrder == "FirstPP" ? "date_desc" : "Date";
            ViewBag.FirstPP = sortOrder == "SecondPP" ? "date_desc" : "Date";

            var pp = from p in list.Postpartum select p;
            switch (sortOrder)
            {
                case "FirstPP":
                    pp = pp.OrderByDescending(s => s.within24hrs_after_delivery);
                    break;
                case "SecondPP":
                    pp = pp.OrderByDescending(s => s.within1week_after_delivery);
                    break;
            }
            return PartialView("MotherList", pp.ToList());
        }

        #endregion

        #region Postpartum sorting
        public ActionResult PostpartumList(String sortOrder)
        {

            if (Session["Login"] == null)
            {
                TempData["sessionIsExpired"] = "Your session has expired.";
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.MotherID = String.IsNullOrEmpty(sortOrder) ? "MotherID" : "";
                ViewBag.Within24hrs = String.IsNullOrEmpty(sortOrder) ? "Within24hrs" : "";
                ViewBag.Within1Week = String.IsNullOrEmpty(sortOrder) ? "Within1Week" : "";

                var mother = from s in list.Postpartum
                             select s;
                switch (sortOrder)
                {
                    case "MotherID":
                        mother = mother.OrderByDescending(s => s.mother_id);
                        break;
                    case "Within24hrs":
                        mother = mother.OrderByDescending(s => s.within24hrs_after_delivery);
                        break;

                    case "Within1Week":
                        mother = mother.OrderByDescending(s => s.within1week_after_delivery);
                        break;


                    default:
                        mother = mother.OrderBy(s => s.mother_id);
                        break;
                }
                return View(mother.ToList());

            }

        }
        #endregion

        #region Search postpartum

        public ActionResult searchPostpartum(String SearchString)
        {

            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Mother mother = list.Mother.Where(i => i.first_name.Contains(SearchString)
                            || i.last_name.Contains(SearchString)).FirstOrDefault();

                if (mother != null)
                {
                    var postpartum = list.Postpartum.Where(i => i.mother_id == mother.mother_id);
                    return View("PostpartumList", postpartum.ToList());

                }
                else
                {
                    ViewBag.SearchNotFound = "Opps, search not found.";
                    var postpartum = from s in list.Postpartum select s;
                    return View("PostpartumList", postpartum.ToList());

                }
            }
        }


        #endregion

        #region checkPostpartum sched

        public PartialViewResult PospartumAlert()
        {
            var tomorrow = DateTime.Today.AddDays(1);

            var count = list.Postpartum.Any(
                        a => a.within24hrs_after_delivery.Equals(tomorrow) ||
                        a.within24hrs_after_delivery.Equals(tomorrow)
                        );

            if (count)
            {
                ViewBag.newEvent = "empty";
            }
            return PartialView();

        }

        #endregion
    }
}