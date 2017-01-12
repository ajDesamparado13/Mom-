using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using MomPlusAdmin.Models;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Repository.AdminRepo;
using System.Data;
using MomPlusAdmin.Controllers.Homepage;

namespace MomPlusAdmin.Controllers
{
    public class CalendarController : Controller
    {
        private MomPlusDbContext context = new MomPlusDbContext();
        
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2016, 1, 1);
            return View(sched);

        }

        public ContentResult Data()
        {
            var item = from c in context.Calendar
                       select new CalendarEvent
                       {
                           id = c.notif_id,
                           start_date = c.start_date,
                           end_date = c.end_date,
                           text = "Event Name: "+ c.Event,
                           
                       };
            return (new SchedulerAjaxData(item.ToList()));
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            AdminRepo admin = new AdminRepo();
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (Calendar)DHXEventsHelper.Bind(typeof(Calendar), actionValues);
                var insertion = (CalendarEvent)DHXEventsHelper.Bind(typeof(CalendarEvent), actionValues);


                switch (action.Type)
                {
                    case DataActionTypes.Insert:

                        Calendar calendar = new Calendar()
                        {

                            start_date = insertion.start_date,
                            end_date = insertion.end_date,
                            Event = insertion.text,
                            
                        };

                        admin.saveCalendar(calendar);
                        break;

                    case DataActionTypes.Delete:
                        admin.deleteCalendar(id);
                        break;

                    default:

                        Calendar update = context.Calendar.Where(i => i.notif_id == id).FirstOrDefault();

                        update.notif_id = id;
                        update.start_date = insertion.start_date;
                        update.end_date = insertion.end_date;
                        update.Event = insertion.text;

                        admin.updateCalendar(update);
                        break;
                }
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
            
        }

        public ActionResult reloadPage()
        {
            return View("Homepage");
        }
    }
}