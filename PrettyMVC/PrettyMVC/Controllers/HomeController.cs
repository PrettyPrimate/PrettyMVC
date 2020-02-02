using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PrettyMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {





            return View();
        }

        public JsonResult GetClubDetails()
        {

            return new JsonResult();
        }

        //[HttpPost]
        //public async Task<JsonResult> UpdateTarget(UpdateStaffSalesTarget command)
        //{
        //    return this.Json(await this.ExecuteJSONCommand(command), JsonRequestBehavior.AllowGet);
        //}

        public async Task<ActionResult> GetClubs()
        {
            return new EmptyResult();
        }
    }
}