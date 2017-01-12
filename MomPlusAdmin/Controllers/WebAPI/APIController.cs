using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using MomPlusAdmin.Domain.Context;

namespace MomPlusAdmin.Controllers.WebAPI
{
    public class APIController : ApiController
    {
        public JsonResult Get()
        {

            MomPlusDbContext context = new MomPlusDbContext();
            var mother = from s in context.Mother select s;


            var results = context.Mother.Select(e => new
            {
                id = e.mother_id,
                FirstName = e.first_name,
                LastName = e.last_name,
                Address = e.address,
                ContactNum = e.contact_num,
                Birthdate = e.birth_date,
                Username = e.username,
                Password = e.password,
            }).ToList();

            
            return new JsonResult() { Data = results, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
